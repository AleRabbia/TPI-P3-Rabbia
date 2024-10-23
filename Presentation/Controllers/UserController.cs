using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "SysAdmin")]
    [HttpGet]

    public ActionResult<ICollection<UserDto>> GetAll()
    {
        var users = _userService.GetAllUsers();
        var userDtos = users.Select(user => UserDto.Create(user)).ToList();
        return Ok(userDtos);
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetById(int id)
    {
        
        try
        {
            if (bool.Parse(User.FindFirst("Enabled")?.Value))
            {Console.WriteLine("Usuario habilitado");}
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _userService.GetUserById(id, userId);
            var userDto = UserDto.Create(user);
            return Ok(userDto);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
    }
    [Authorize(Roles = "SysAdmin,Admin")]
    [HttpGet("enabled")]
    public ActionResult<IEnumerable<UserDto>> GetEnabledUsers()
    {      
        var users = _userService.GetEnabledUsers();
        return Ok(users);
    }

    [Authorize(Roles = "SysAdmin")]
    [HttpPost("create")]
    public ActionResult Create(CreateUserDto userDto)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole == "SysAdmin")
        {
            var user = new User
            {
                Name = userDto.Name,           
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                Enabled = true
            };

            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, UserDto.Create(user));
        }
            return Forbid();
              
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public ActionResult RegisterUser(RegisterUserDto registerUserDto)
    {
               var user = new User
        {
            Name = registerUserDto.Name,           
            Email = registerUserDto.Email,
            Password = registerUserDto.Password,
            Role = "Customer",
            Enabled = true
        };

        _userService.AddUser(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, UserDto.Create(user));
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateUserDto userDto)
    {

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var existingUser = _userService.GetUserById(id, userId);
            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.Password = userDto.Password;
            existingUser.Role = userDto.Role;
    
            _userService.UpdateUser(existingUser);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    [Authorize(Roles ="SysAdmin")]
    [HttpPatch("admin/{id}")]
    public ActionResult Update(int id, UpdateuserDtoAdmin userDto)
    {
        var existingUser = _userService.GetUserById(id, null);
        if (existingUser == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        if (userDto.Name != null)
         {
             existingUser.Name = userDto.Name;
         }

        if (userDto.Email!= null)
        {
            existingUser.Email = userDto.Email;
        }
    
        if (userDto.Password!= null)
        {
            existingUser.Password = userDto.Password;
        }
    
        if (userDto.Role!= null)
        {
            existingUser.Role = userDto.Role;
        }
    
        if (userDto.Enabled.HasValue)
        {
            existingUser.Enabled = userDto.Enabled.Value;
        }
        _userService.DeleteUserLogic(existingUser);
        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
    
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var enabledClaim = User.FindFirst("enabled")?.Value;

            if (enabledClaim == null || !bool.Parse(enabledClaim))
            {
                throw new KeyNotFoundException("Usuario no Habilitado.");
            }
            else
            {                           
                var existingUser = _userService.GetUserById(id, userId);
        
                _userService.DeleteUser(existingUser.Id);
                return NoContent();
                }
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

      //  if(userRole != nameof(UserRole.Admin) && userRole != nameof(UserRole.Customer))
             //   return Forbid();

        
    }
}
