namespace university.Models
{
    public class Reviews
    {
        /// <summary>
        /// Id отзыва.
        /// </summary>
        public int ReviewsId { get; set; }
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
}
