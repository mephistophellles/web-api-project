namespace university.Models
{
    public class RegistrationCourse
    {
        /// <summary>
        /// Id регистрации на курсы.
        /// </summary>
        public int RegistrationCourseId { get; set; }
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
}
