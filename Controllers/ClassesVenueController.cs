using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.Models;

namespace university.Controllers;

[ApiController]
public class ClassesVenueController : ControllerBase
{
    private readonly DbHelper database;

    public ClassesVenueController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/classesvenue")]
    public async Task<IActionResult> AddVenue([FromBody] ClassesVenue classVenue)
    {
        if (await database.Classess.FindAsync(classVenue.ClassesId) is null
            || await database.Venue.FindAsync(classVenue.VenueId) is null)
        {
            return BadRequest("Занятие или помещение не найдено");
        }

        if (await database.ClassesVenue.FindAsync(
                classVenue.ClassesId, classVenue.VenueId) is null)
        {
            return Conflict("Для данного занятия уже назначен данное помещение");
        }

        await database.ClassesVenue.AddAsync(classVenue);
        await database.SaveChangesAsync();

        return Ok(classVenue);
    }

    [Authorize]
    [HttpGet("/api/classesvenue")]
    public async Task<IActionResult> GetById(int classId, int venueId)
    {
        var classVenue = await database.ClassesVenue.FindAsync(classId, venueId);
        if (classVenue is null)
        {
            return NotFound();
        }

        return Ok(classVenue);
    }

    [Authorize]
    [HttpGet("/api/classesvenue/all")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var classVenue = await database.ClassesVenue
            .Skip(skip)
            .Take(take)
            .OrderBy(classVenue => classVenue.ClassesId)
            .ToListAsync();

        return Ok(classVenue);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/classesvenue")]
    public async Task<IActionResult> Delete(int classId, int venueId)
    {
        var classVenue = await database.ClassesVenue.FindAsync(classId, venueId);
        if (classVenue is null)
        {
            return BadRequest();
        }

        database.ClassesVenue.Remove(classVenue);
        await database.SaveChangesAsync();

        return Ok();
    }
}