using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class RegistrationCourseController : ControllerBase
{
    private readonly DbHelper database;

    public RegistrationCourseController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/registercourse")]
    public async Task<IActionResult> Add([FromBody] RegistrationCourseQm registerCourseQm)
    {
        if (await database.Students.FindAsync(registerCourseQm.StudentId) is null
            || await database.Courses.FindAsync(registerCourseQm.CourseId) is null)
        {
            return BadRequest("Студент или курс не найден");
        }

        var registerCourse = new RegistrationCourse
        {
            StudentId = registerCourseQm.StudentId,
            CourseId = registerCourseQm.CourseId,
            Date = registerCourseQm.Date
        };

        await database.RegistrationCourse.AddAsync(registerCourse);
        await database.SaveChangesAsync();

        return Ok(registerCourse);
    }

    [Authorize]
    [HttpGet("/api/registercourse/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var registerCourse = await database.RegistrationCourse.FindAsync(id);
        if (registerCourse is null)
        {
            return NotFound("Регистрация не найдена");
        }

        return Ok(registerCourse);
    }

    [Authorize]
    [HttpGet("/api/registercourse")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var registerCourses = await database.RegistrationCourse
            .Skip(skip)
            .Take(take)
            .OrderBy(r => r.Date)
            .ToListAsync();

        return Ok(registerCourses);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/registercourse/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RegistrationCourseQm registerCourseQm)
    {
        var registerCourse = await database.RegistrationCourse.FindAsync(id);
        if (registerCourse is null)
        {
            return BadRequest("Регистрация не найдена");
        }

        registerCourse.StudentId = registerCourseQm.StudentId;
        registerCourse.CourseId = registerCourseQm.CourseId;
        registerCourse.Date = registerCourseQm.Date;
        await database.SaveChangesAsync();

        return Ok(registerCourse);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/registercourse/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var registerCourse = await database.RegistrationCourse.FindAsync(id);
        if (registerCourse is null)
        {
            return BadRequest("Регистрация не найдена");
        }

        database.RegistrationCourse.Remove(registerCourse);

        return Ok();
    }
}