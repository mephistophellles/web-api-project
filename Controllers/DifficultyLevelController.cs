using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.Models;

namespace university.Controllers;

[ApiController]
public class DifficultyLevelController : ControllerBase
{
    private readonly DbHelper database;

    public DifficultyLevelController(DbHelper database)
    {
        this.database = database;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("api/difficultyLevels")]
    public async Task<IActionResult> AddDifficultyLevel([FromBody] DifficultyLevel difficultyLevelQm)
    {
        var difLevel = new DifficultyLevel
        {
            Name = difficultyLevelQm.Name,
            Description = difficultyLevelQm.Description
        };

        await database.DifficultyLevels.AddAsync(difLevel);
        await database.SaveChangesAsync();

        return Ok(difLevel);
    }

    [Authorize]
    [HttpGet("/api/difficultyLevels/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var difLevel = await database.DifficultyLevels.FindAsync(id);
        if (difLevel is null)
        {
            return NotFound("Уровень сложности не найден");
        }

        return Ok(difLevel);
    }

    [Authorize]
    [HttpGet("/api/difficultyLevels")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
    {
        var difLevels = await database.DifficultyLevels
            .Skip(skip)
            .Take(take)
            .OrderBy(d => d.DifficultyLevelId)
            .ToListAsync();

        return Ok(difLevels);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/api/difficultyLevels/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DifficultyLevel difficultyLevelQm)
    {
        var difLevel = await database.DifficultyLevels.FindAsync(id);
        if (difLevel is null)
        {
            return BadRequest("Уровень сложности не найден");
        }

        difLevel.Name = difficultyLevelQm.Name;
        difLevel.Description = difficultyLevelQm.Description;
        await database.SaveChangesAsync();

        return Ok(difLevel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/difficultyLevels/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var difLevel = await database.DifficultyLevels.FindAsync(id);
        if (difLevel is null)
        {
            return BadRequest("Уровень сложности не найден");
        }

        database.DifficultyLevels.Remove(difLevel);

        return Ok();
    }
}