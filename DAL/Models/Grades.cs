namespace university.Models
{
    public class Grades
    {
        /// <summary>
        /// Id оценки. 
        /// </summary>
        public int GradesId { get; set; }
        /// <summary>
        /// Id студента.
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Id курса.
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// Оценка.
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// Комментарий к поставленной оценке.
        /// </summary>
        public string? Comment { get; set; }
    }
}
