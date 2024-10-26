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

    
    [Authorize(Roles = "SysAdmin, Admin")]
    [HttpGet]

    public ActionResult<ICollection<FieldDto>> GetAllFields()
    {
        try {
        var fields = _fieldService.GetAllFields();
        var fieldDtos = fields.Select(field => FieldDto.CreateField(field)).ToList();
        return Ok(fieldDtos);  
        }
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }
    

    [HttpGet("{id}")]
    public ActionResult<FieldDto> GetFieldById([FromRoute]int id)
    {
        try{
        var field = _fieldService.GetFieldById(id);
        if (field == null)
        {
            return NotFound();
        }

        var fieldDto = FieldDto.CreateField(field);
        return Ok(fieldDto);
        }
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }


    [HttpGet("enabled")]
    public ActionResult<IEnumerable<FieldDto>> GetEnabledField()
    {
        try{
        var fields = _fieldService.GetEnabledFields();
        var fieldDtos = fields.Select(field => FieldDto.CreateField(field)).ToList();
        return Ok(fieldDtos); 
        }
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }          

    }

    [Authorize(Roles = "SysAdmin, Admin")]
    [HttpPost]
    public ActionResult CreateField([FromBody]CreateFieldDto fieldDto)
    {
        try{
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
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }
    
    [Authorize(Roles = "SysAdmin, Admin")]
    [HttpPut("{id}")]
    public ActionResult UpdateField([FromRoute]int id,[FromBody] UpdateFieldDto fieldDto)
    {
        try {
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
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }
    [Authorize(Roles = "SysAdmin")]
    [HttpPatch("admin/{id}")]
    public ActionResult Update([FromRoute]int id, [FromBody]UpdateFieldDtoAdmin fieldDto)
    {
        try {
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
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }

    [Authorize(Roles = "SysAdmin, Admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute]int id)
    {
        try {
        var existingField = _fieldService.GetFieldById(id);
        if (existingField == null)
        {
            return NotFound("Campo no encontrado.");
        }

        _fieldService.DeleteField(id);
        return NoContent();
        }
        catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }
}
