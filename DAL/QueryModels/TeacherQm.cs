namespace university.DAL.QueryModels;

public class TeacherQm
{
    /// <summary>
    /// Фамилия, имя, отчество преподавателя.
    /// </summary>
    public string? Fio { get; set; }
    /// <summary>
    /// Стаж работы.
    /// </summary>
    public int Internship { get; set; }
    /// <summary>
    /// Контактная информация преподавателя.
    /// </summary>
    public string? ContactInformation { get; set; }
}