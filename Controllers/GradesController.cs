using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class GradesController : ControllerBase
{
    private readonly DbHelper database;

    public GradesController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpPost("api/grades")]
    public async Task<IActionResult> Add([FromBody] GradesQm gradesQm)
    {
        if (await database.Students.FindAsync(gradesQm.StudentId) is null
            || database.Courses.Find(gradesQm.CourseId) is null)
        {
            return BadRequest("Студент или курс не найден");
        }

        var grade = new Grades
        {
            StudentId = gradesQm.StudentId,
            CourseId = gradesQm.CourseId,
            Grade = gradesQm.Grade,
            Comment = gradesQm.Comment
        };

        await database.Grades.AddAsync(grade);
        await database.SaveChangesAsync();

        return Ok(grade);
    }

    [Authorize]
    [HttpGet("api/grades/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var grade = await database.Grades.FindAsync(id);
        if (grade is null)
        {
            return NotFound("Оценка не найдена");
        }

        return Ok(grade);
    }

    [Authorize]
    [HttpGet("api/grades")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var grades = await database.Grades
            .Skip(skip)
            .Take(take)
            .OrderBy(g => g.StudentId)
            .ToListAsync();

        return Ok(grades);
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpPut("api/grades/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GradesQm gradesQm)
    {
        var grade = await database.Grades.FindAsync(id);
        if (grade is null)
        {
            return BadRequest("Оценка не найдена");
        }

        grade.Grade = gradesQm.Grade;
        grade.Comment = gradesQm.Comment;
        grade.StudentId = gradesQm.StudentId;
        grade.CourseId = gradesQm.CourseId;
        await database.SaveChangesAsync();

        return Ok(grade);
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpDelete("api/grades/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var grade = await database.Grades.FindAsync(id);
        if (grade is null)
        {
            return BadRequest("Оценка не найдена");
        }

        database.Grades.Remove(grade);

        return Ok();
    }
}