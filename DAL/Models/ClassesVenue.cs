using Microsoft.EntityFrameworkCore;

namespace university.Models
{
    [PrimaryKey(nameof(ClassesId), nameof(VenueId))]
    public class ClassesVenue
    {
        /// <summary>
        /// Id занятия.
        /// </summary>
        public int ClassesId { get; set; }
        /// <summary>
        /// Id помещения.
        /// </summary>
        public int VenueId { get; set; }
    }
}
