using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Do2Day.Models;
using System.Net.Http;
using System.Text.Json;

namespace What2Do2Day.Pages.Anime
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnimeItem Anime { get; set; }
        public string EpisodeUrl { get; set; }

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AniList");

            string query = @"
                query ($id: Int) {
                    Media(id: $id, type: ANIME) {
                        id
                        title {
                            romaji
                        }
                        coverImage {
                            large
                        }
                        description
                        genres
                    }
                }";

            var requestBody = new
            {
                query,
                variables = new { id }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var aniListResponse = JsonSerializer.Deserialize<AniListDetailResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var media = aniListResponse.Data.Media;
            Anime = new AnimeItem
            {
                Id = media.Id,
                Title = media.Title.Romaji,
                CoverImage = media.CoverImage.Large,
                Description = media.Description,
                Genres = media.Genres,
                EpisodeUrl = "https://www.crunchyroll.com/watch/GY5PWWP86/placeholder" // Placeholder; replace with real logic
            };
            EpisodeUrl = Anime.EpisodeUrl;
        }
    }

    // Helper classes for deserializing AniList response
    public class AniListDetailResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Media Media { get; set; }
    }

    public class Media
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public CoverImage CoverImage { get; set; }
        public string Description { get; set; }
        public string[] Genres { get; set; }
    }

    public class Title
    {
        public string Romaji { get; set; }
    }

    public class CoverImage
    {
        public string Large { get; set; }
    }
}