using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class ReviewController : ControllerBase
{
    private readonly DbHelper database;

    public ReviewController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin, Student")]
    [HttpPost("/api/review")]
    public async Task<IActionResult> Add([FromBody] ReviewQm reviewQm)
    {
        if (await database.Students.FindAsync(reviewQm.StudentId) is null
            || await database.Courses.FindAsync(reviewQm.CourseId) is null)
        {
            return BadRequest("Ученик или курс не найден");
        }

        var review = new Reviews
        {
            StudentId = reviewQm.StudentId,
            CourseId = reviewQm.CourseId,
            Text = reviewQm.Text
        };

        await database.Reviews.AddAsync(review);
        await database.SaveChangesAsync();

        return Ok(review);
    }

    [Authorize]
    [HttpGet("/api/review/{id:int}")]
    public async Task<IActionResult> GetByUd(int id)
    {
        var review = await database.Reviews.FindAsync(id);
        if (review is null)
        {
            return NotFound("Отзыв не найден");
        }

        return Ok(review);
    }

    [Authorize]
    [HttpGet("/api/review")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var reviews = await database.Reviews
            .Skip(skip)
            .Take(take)
            .OrderBy(r => r.StudentId)
            .ToListAsync();

        return Ok(reviews);
    }

    [Authorize(Roles = "Admin, Student")]
    [HttpPut("/api/review/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReviewQm reviewQm)
    {
        var review = await database.Reviews.FindAsync(id);
        if (review is null)
        {
            return NotFound("Отзыв не найден");
        }

        review.Text = reviewQm.Text;
        review.StudentId = reviewQm.StudentId;
        review.CourseId = reviewQm.CourseId;
        await database.SaveChangesAsync();

        return Ok(review);
    }

    [Authorize(Roles = "Admin, Student")]
    [HttpDelete("/api/review/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await database.Reviews.FindAsync(id);
        if (review is null)
        {
            return NotFound("Отзыв не найден");
        }

        database.Reviews.Remove(review);
        await database.SaveChangesAsync();

        return Ok();
    }
}