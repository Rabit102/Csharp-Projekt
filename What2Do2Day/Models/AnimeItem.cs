namespace What2Do2Day.Models
{
    public class AnimeItem
    {
        public string? Title { get; set; }
        public string? CoverImage { get; set; }
        public string? Description { get; set; }
        public string[]? Genres { get; set; }
        public double Rating { get; set; } // double kann nicht null sein, Standardwert ist 0
        public string? TrailerUrl { get; set; }
        public string? Category { get; set; } // Hinzugefügt für Kategorien
    }
}