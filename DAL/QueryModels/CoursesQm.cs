namespace university.DAL.QueryModels;

public class CoursesQm
{
    /// <summary>
    /// Наименование курса.
    /// </summary>
    public string? CourseName { get; set; }
    /// <summary>
    /// Описание курса.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Id уровня сложности.
    /// </summary>
    public int LevelId { get; set; }
}