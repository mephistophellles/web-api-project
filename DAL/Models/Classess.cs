namespace university.Models
{
    public class Classess
    {
        /// <summary>
        /// Id занятия.
        /// </summary>
        public int ClassessId { get; set; }
        /// <summary>
        /// Id курса.
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// Дата проведения занятия.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
