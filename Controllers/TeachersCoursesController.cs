using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.Models;

namespace university.Controllers;

public class TeachersCoursesController : ControllerBase
{
    private readonly DbHelper database;

    public TeachersCoursesController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/teacherscourses")]
    public async Task<IActionResult> Add([FromBody] TeachersCourses teachersCourses)
    {
        if (await database.Teachers.FindAsync(teachersCourses.TeacherId) is null
            || await database.Courses.FindAsync(teachersCourses.CourseId) is null)
        {
            return BadRequest("Преподаватель или курс не найден");
        }

        var modelFromDb = await database.TeachersCourses.FindAsync(
            teachersCourses.TeacherId, teachersCourses.CourseId);
        if (modelFromDb is not null)
        {
            return Conflict("Преподаватель уже ведет этот курс");
        }

        await database.TeachersCourses.AddAsync(teachersCourses);
        await database.SaveChangesAsync();

        return Ok(teachersCourses);
    }

    [Authorize]
    [HttpGet("/api/teacherscourses")]
    public async Task<IActionResult> GetById(int teacherId, int courseId)
    {
        var modelFromDb = await database.TeachersCourses.FindAsync(teacherId, courseId);
        if (modelFromDb is null)
        {
            return NotFound("Преподаватель не ведет данный курс");
        }

        return Ok(modelFromDb);
    }

    [Authorize]
    [HttpGet("/api/teacherscourses/all")]
    public async Task<IActionResult> GetAll(int skip, int take)
    {
        var teachersCourses = await database.TeachersCourses
            .Skip(skip)
            .Take(take)
            .OrderBy(c => c.CourseId)
            .ToListAsync();

        return Ok(teachersCourses);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/teacherscourses")]
    public async Task<IActionResult> Delete(int teacherId, int courseId)
    {
        var modelFromDb = await database.TeachersCourses.FindAsync(teacherId, courseId);
        if (modelFromDb is null)
        {
            return BadRequest("Преподаватель не ведет данный курс");
        }

        database.TeachersCourses.Remove(modelFromDb);
        await database.SaveChangesAsync();

        return Ok(teacherId);
    }
}