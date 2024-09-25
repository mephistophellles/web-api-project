namespace university.Models
{
    public class Courses
    {
        /// <summary>
        /// Id курса.
        /// </summary>
        public int CoursesId { get; set; }
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
}
