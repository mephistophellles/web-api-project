using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class TeachersController : ControllerBase
{
    private readonly DbHelper database;

    public TeachersController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/teachers")]
    public async Task<IActionResult> Add([FromBody] TeacherQm teacherQm)
    {
        var teacher = new Teachers
        {
            Fio = teacherQm.Fio,
            Internship = teacherQm.Internship,
            ContactInformation = teacherQm.ContactInformation
        };

        await database.Teachers.AddAsync(teacher);
        await database.SaveChangesAsync();

        return Ok(teacher);
    }

    [Authorize]
    [HttpGet("/api/teachers/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await database.Teachers.FindAsync(id);
        if (teacher is null)
        {
            return NotFound("Преподаватель не найден");
        }

        return Ok(teacher);
    }

    [Authorize]
    [HttpGet("/api/teachers")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var teachers = await database.Teachers
            .Skip(skip)
            .Take(take)
            .OrderBy(t => t.TeachersId)
            .ToListAsync();

        return Ok(teachers);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/teachers/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TeacherQm teacherQm)
    {
        var teacher = await database.Teachers.FindAsync(id);
        if (teacher is null)
        {
            return BadRequest("Учитель не найден");
        }

        teacher.Fio = teacherQm.Fio;
        teacher.Internship = teacherQm.Internship;
        teacher.ContactInformation = teacherQm.ContactInformation;

        await database.SaveChangesAsync();

        return Ok(teacher);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/teachers/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var teacher = await database.Teachers.FindAsync(id);
        if (teacher is null)
        {
            return BadRequest("Учитель не найден");
        }

        database.Teachers.Remove(teacher);
        await database.SaveChangesAsync();

        return Ok();
    }
}