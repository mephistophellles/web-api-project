using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class ClassesController : ControllerBase
{
    private readonly DbHelper database;

    public ClassesController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/classes")]
    public async Task<IActionResult> Add([FromBody] ClassesQm classesQm)
    {
        if (await database.Courses.FindAsync(classesQm.CourseId) is null)
        {
            return BadRequest("Курс не найден");
        }

        var classes = new Classess()
        {
            CourseId = classesQm.CourseId,
            Date = classesQm.Date
        };

        await database.Classess.AddAsync(classes);
        await database.SaveChangesAsync();

        return Ok(classes);
    }

    [Authorize]
    [HttpGet("/api/classes/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var classes = await database.Classess.FindAsync(id);
        if (classes is null)
        {
            return NotFound("Занятие не найдено");
        }

        return Ok(classes);
    }

    [Authorize]
    [HttpGet("/api/classes")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var classes = await database.Classess
            .Skip(skip)
            .Take(take)
            .OrderBy(c => c.Date)
            .ToListAsync();

        return Ok(classes);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/classes/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClassesQm classesQm)
    {
        var classes = await database.Classess.FindAsync(id);
        if (classes is null)
        {
            return BadRequest("Занятие не найдено");
        }

        classes.Date = classesQm.Date;
        classes.CourseId = classesQm.CourseId;
        await database.SaveChangesAsync();

        return Ok(classes);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/classes/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var classes = await database.Classess.FindAsync(id);
        if (classes is null)
        {
            return BadRequest("Занятие не найдено");
        }

        database.Classess.Remove(classes);
        await database.SaveChangesAsync();

        return Ok();
    }
}