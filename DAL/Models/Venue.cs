namespace university.Models
{
    public class Venue
    {
        /// <summary>
        /// Id помещения.
        /// </summary>
        public int VenueId { get; set; }
        /// <summary>
        /// Город, в котором находится помещение.
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// Район, в котором находится помещение.
        /// </summary>
        public string? District { get; set; }
        /// <summary>
        /// Улица расположения помещения.
        /// </summary>
        public string? Street { get; set; }
        /// <summary>
        /// Номер дома.
        /// </summary>
        public int House { get; set; }
        /// <summary>
        /// Номер этажа.
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// Номер кабинета.
        /// </summary>
        public int Office { get; set; }
    }
}
