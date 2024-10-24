using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Presentation.Controllers;
[ApiController]
[Route("api/field")]
public class FieldController : ControllerBase
{
    private readonly IFieldService _fieldService;

    public FieldController(IFieldService fieldService)
    {
        _fieldService = fieldService;
    }

    [HttpGet]

    public ActionResult<ICollection<FieldDto>> GetAllFields()
    {
       // int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        //if (userRole != nameof(UserRole.Admin))
            //return Forbid();

        var fields = _fieldService.GetAllFields();
        var fieldDtos = fields.Select(field => FieldDto.CreateField(field)).ToList();
        return Ok(fieldDtos);  
    }
    
[HttpGet("{id}")]
    public ActionResult<FieldDto> GetFieldById(int id)
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

       // if (userRole != nameof(UserRole.Admin) && userId != id)
         //   return Forbid();

        var field = _fieldService.GetFieldById(id);
        if (field == null)
        {
            return NotFound();
        }

        var fieldDto = FieldDto.CreateField(field);
        return Ok(fieldDto);
    }


    [HttpGet("enabled")]
    public ActionResult<IEnumerable<FieldDto>> GetEnabledField()
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

       // if (userRole != nameof(UserRole.Admin))
           // return Forbid();
        var fields = _fieldService.GetEnabledFields();
        var fieldDtos = fields.Select(field => FieldDto.CreateField(field)).ToList();
        return Ok(fieldDtos);           


        //var fields = _fieldService.GetEnabledFields();
        //return Ok(fields);
    }

    [HttpPost]
    public ActionResult CreateField(CreateFieldDto fieldDto)
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        //if (userRole != nameof(UserRole.Admin))
            //return Forbid();

       // if (!Enum.TryParse(userDto.Role, out UserRole role))
        //{
          //  return BadRequest("Invalid role.");
       // }

        var field = new Field
        {
            Name = fieldDto.Name,           
            Price = fieldDto.Price,
            Type = fieldDto.Type,
            DurationInHours = fieldDto.DurationInHours,
            Enabled = true
        };

        _fieldService.AddField(field);
        return CreatedAtAction(nameof(GetFieldById), new { id = field.Id }, FieldDto.CreateField(field));
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateField(int id, UpdateFieldDto fieldDto)
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

//        if (userRole != nameof(UserRole.Admin) && userRole != nameof(UserRole.Customer) && userId != id)
  //          return Forbid();


       // if (!Enum.TryParse(userDto.Role, out UserRole role))
       // {            return BadRequest("Invalid role.");        }

        var existingField = _fieldService.GetFieldById(id);
        if (existingField == null)
        {
            return NotFound("Campo no encontrado.");
        }

    
        existingField.Name = fieldDto.Name;
        existingField.Type = fieldDto.Type;
        existingField.Price = fieldDto.Price;  
        
        _fieldService.UpdateField(existingField);
        return NoContent();
    }

   [HttpPatch("admin/{id}")]
    public ActionResult Update(int id, UpdateFieldDtoAdmin fieldDto)
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

//        if (userRole != nameof(UserRole.Admin) && userRole != nameof(UserRole.Customer) && userId != id)
  //          return Forbid();


       // if (!Enum.TryParse(userDto.Role, out UserRole role))
       // {            return BadRequest("Invalid role.");        }

        var existingField = _fieldService.GetFieldById(id);
        if (existingField == null)
        {
            return NotFound("Campo no encontrado.");
        }

            if (fieldDto.Name != null)
    {
        existingField.Name = fieldDto.Name;
    }

     if (fieldDto.Type != null)
    {
        existingField.Type = fieldDto.Type;
    }
    if(fieldDto.Price.HasValue)
    {
    existingField.Price = fieldDto.Price.Value;
    }
    if(fieldDto.DurationInHours.HasValue)
    {
        existingField.DurationInHours = fieldDto.DurationInHours.Value;
    }
    if (fieldDto.Enabled.HasValue)
    {
        existingField.Enabled = fieldDto.Enabled.Value;
    }
        _fieldService.DeleteFieldLogic(existingField);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
        //var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

      //  if(userRole != nameof(UserRole.Admin) && userRole != nameof(UserRole.Customer))
             //   return Forbid();

        var existingField = _fieldService.GetFieldById(id);
        if (existingField == null)
        {
            return NotFound("Campo no encontrado.");
        }

        _fieldService.DeleteField(id);
        return NoContent();
    }
}
