namespace university.DAL.QueryModels;

public class RegistrationCourseQm
{
    /// <summary>
    /// Id студента.
    /// </summary>
    public int StudentId { get; set; }
    /// <summary>
    /// Id курса.
    /// </summary>
    public int CourseId { get; set; }
    /// <summary>
    /// Дата регистрации.
    /// </summary>
    public DateTime Date { get; set; }
}