namespace university.Models
{
    public class Students
    {
        /// <summary>
        /// Id студента.
        /// </summary>
        public int StudentsId { get; set; }
        /// <summary>
        /// Фамилия, имя, отчество студента.
        /// </summary>
        public string? Fio { get; set; }
        /// <summary>
        /// Дата рождения студента.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
