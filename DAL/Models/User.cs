using university.DAL.Enums;

namespace university.DAL.Models;

public class User
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Соль
    /// </summary>
    public string Salt { get; set; }
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public Role Role { get; set; }
}