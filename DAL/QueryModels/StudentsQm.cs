namespace university.DAL.QueryModels;

public class StudentsQm
{
    /// <summary>
    /// Фамилия, имя, отчество студента.
    /// </summary>
    public string? Fio { get; set; }
    /// <summary>
    /// Дата рождения студента.
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}