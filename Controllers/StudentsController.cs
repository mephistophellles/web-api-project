using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

public class StudentsController : ControllerBase
{
    private readonly DbHelper database;

    public StudentsController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/students")]
    public async Task<IActionResult> Add([FromBody] StudentsQm studentsQm)
    {
        var modelFromDb = await database.Students
            .FirstOrDefaultAsync(s => s.DateOfBirth == studentsQm.DateOfBirth
                                      && s.Fio == studentsQm.Fio);
        if (modelFromDb is not null)
        {
            return Conflict("Студент уже зарегистрирован");
        }

        var student = new Students
        {
            Fio = studentsQm.Fio,
            DateOfBirth = studentsQm.DateOfBirth
        };

        await database.Students.AddAsync(student);
        await database.SaveChangesAsync();

        return Ok(student);
    }

    [Authorize]
    [HttpGet("/api/students/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var modelFromDb = await database.Students.FindAsync(id);
        if (modelFromDb is null)
        {
            return NotFound("Студент не найден");
        }

        return Ok(modelFromDb);
    }

    [Authorize]
    [HttpGet("/api/students")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var students = await database.Students
            .Skip(skip)
            .Take(take)
            .OrderBy(s => s.DateOfBirth)
            .ToListAsync();

        return Ok(students);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/students/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentsQm studentsQm)
    {
        var modelFromDb = await database.Students.FindAsync(id);
        if (modelFromDb is null)
        {
            return BadRequest("Студент не найден");
        }

        modelFromDb.DateOfBirth = studentsQm.DateOfBirth;
        modelFromDb.Fio = studentsQm.Fio;
        await database.SaveChangesAsync();

        return Ok(modelFromDb);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/students/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var modelFromDb = await database.Students.FindAsync(id);
        if (modelFromDb is null)
        {
            return BadRequest("Студент не найден");
        }

        database.Students.Remove(modelFromDb);

        return Ok();
    }
}