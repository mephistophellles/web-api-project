namespace university.Models
{
    public class DifficultyLevel
    {
        /// <summary>
        /// Id уровня сложности.
        /// </summary>
        public int DifficultyLevelId { get; set; }
        /// <summary>
        /// Наименование уровня.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Описание уровня.
        /// </summary>
        public string? Description { get; set; }
    }
}
