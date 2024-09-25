using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace Zakaz.Controllers;

[ApiController]
public class CoursesController : ControllerBase
{
    private readonly DbHelper database;

    public CoursesController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize]
    [Authorize(Roles = "Admin")]
    [HttpPost("/api/courses")]
    public async Task<IActionResult> Add([FromBody] CoursesQm courseQm)
    {
        var course = new Courses
        { CourseName = courseQm.CourseName, LevelId = courseQm.LevelId, Description = courseQm.Description };
        if (await database.DifficultyLevels.FindAsync(course.LevelId) is null)
        {
            return BadRequest("Такого уровня сложности нет");
        }

        await database.Courses.AddAsync(course);
        await database.SaveChangesAsync();

        return Ok(course.CoursesId);
    }

    [Authorize]
    [HttpGet("/api/courses/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await database.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound("Такого курса нет");
        }

        return Ok(course);
    }

    [Authorize]
    [HttpGet("/api/courses")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var courses = await database.Courses
            .Skip(skip)
            .Take(take)
            .OrderBy(c => c.CoursesId)
            .ToListAsync();

        return Ok(courses);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/courses/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CoursesQm courseQm)
    {
        var course = await database.Courses.FindAsync(id);
        if (course is null)
        {
            return BadRequest("Курс не найден");
        }
        if (await database.DifficultyLevels.FindAsync(course.LevelId) is null)
        {
            return BadRequest("Такого уровня сложности нет");
        }

        course.CourseName = courseQm.CourseName;
        course.LevelId = courseQm.LevelId;
        course.Description = courseQm.Description;

        await database.SaveChangesAsync();

        return Ok(course);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/courses/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await database.Courses.FindAsync(id);
        if (course is null)
        {
            return BadRequest("Курс не найден");
        }

        database.Courses.Remove(course);
        await database.SaveChangesAsync();

        return Ok();
    }
}