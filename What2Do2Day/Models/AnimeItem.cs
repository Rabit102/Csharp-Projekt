// Models/AnimeItem.cs
namespace What2Do2Day.Models
{
    public class AnimeItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public string[] Genres { get; set; }
        public string EpisodeUrl { get; set; }
    }
}