using Microsoft.EntityFrameworkCore;

namespace university.Models
{
    [PrimaryKey(nameof(TeacherId), nameof(CourseId))]
    public class TeachersCourses
    {
        /// <summary>
        /// Id преподавателя.
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// Id курса.
        /// </summary>
        public int CourseId { get; set; }
    }
}
