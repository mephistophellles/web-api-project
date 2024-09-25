using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.QueryModels;
using university.Models;

namespace university.Controllers;

[ApiController]
public class VenueController : ControllerBase
{
    private readonly DbHelper database;

    public VenueController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/venue")]
    public async Task<IActionResult> Add([FromBody] VenueQm venueQm)
    {
        var venue = new Venue
        {
            City = venueQm.City,
            District = venueQm.District,
            Street = venueQm.Street,
            House = venueQm.House,
            Floor = venueQm.Floor,
            Office = venueQm.Office
        };

        await database.Venue.AddAsync(venue);
        await database.SaveChangesAsync();

        return Ok(venue);
    }

    [Authorize]
    [HttpGet("/api/venue/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var venue = await database.Venue.FindAsync(id);
        if (venue is null)
        {
            return NotFound("Помещение не найдено");
        }

        return Ok(venue);
    }

    [Authorize]
    [HttpGet("/api/venue")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var venues = await database.Venue
            .Skip(skip)
            .Take(take)
            .OrderBy(v => v.VenueId)
            .ToListAsync();

        return Ok(venues);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/venue/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VenueQm venueQm)
    {
        var venue = await database.Venue.FindAsync(id);
        if (venue is null)
        {
            return BadRequest("Помещение не найдено");
        }

        venue.City = venueQm.City;
        venue.District = venueQm.District;
        venue.Street = venueQm.Street;
        venue.House = venueQm.House;
        venue.Floor = venueQm.Floor;
        venue.Office = venueQm.Office;
        await database.SaveChangesAsync();

        return Ok(venue);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/venue/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var venue = await database.Venue.FindAsync(id);
        if (venue is null)
        {
            return BadRequest("Помещение не найдено");
        }

        database.Venue.Remove(venue);

        return Ok();
    }
}