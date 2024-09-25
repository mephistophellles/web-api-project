using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using university.DAL;
using university.DAL.Enums;
using university.DAL.Models;
using university.DAL.QueryModels;
using university.Services;

namespace university.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly DbHelper dataBase;
    private readonly JwtCreator jwtCreator;
    private readonly PasswordEncrypt passwordEncrypt;


    public UserController(DbHelper dataBase, JwtCreator jwtCreator,
        PasswordEncrypt passwordEncrypt)
    {
        this.dataBase = dataBase;
        this.jwtCreator = jwtCreator;
        this.passwordEncrypt = passwordEncrypt;
    }

    [HttpPost("/api/user/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegister userRegister)
    {
        var userFromDb = await dataBase.Users.FirstOrDefaultAsync(u => u.Username == userRegister.Username);
        if (userFromDb is not null)
        {
            return Conflict("Пользователь с данным именем уже существует");
        }

        if (userRegister.Role == Role.NotSetted)
        {
            return BadRequest("Не указана роль");
        }

        var salt = Guid.NewGuid().ToString();
        var password = passwordEncrypt.Encrypt(userRegister.Password, salt);

        var user = new User()
        {
            Username = userRegister.Username,
            Password = password,
            Salt = salt,
            Role = userRegister.Role,
        };

        await dataBase.Users.AddAsync(user);
        await dataBase.SaveChangesAsync();

        return Ok(user.Id);
    }

    [HttpPost("/api/user/login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLogin userLogin)
    {
        var user = await dataBase.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username);
        if (user is null)
        {
            return NotFound("Пользователь не найден");
        }

        if (passwordEncrypt.Encrypt(userLogin.Password, user.Salt) != user.Password)
        {
            return Unauthorized("Неверное имя или пароль");
        }

        var jwt = jwtCreator.CreateToken(user.Id, user.Role);

        return Ok(jwt);
    }
}