namespace university.DAL.QueryModels;

public class ReviewQm
{
    /// <summary>
    /// Id студента, который отправляет отзыв.
    /// </summary>
    public int StudentId { get; set; }
    /// <summary>
    /// Id курса студента.
    /// </summary>
    public int CourseId { get; set; }
    /// <summary>
    /// Текст отзыва.
    /// </summary>
    public string? Text { get; set; }
}