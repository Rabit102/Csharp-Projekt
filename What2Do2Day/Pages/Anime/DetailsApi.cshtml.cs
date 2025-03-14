using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Do2Day.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace What2Do2Day.Pages.Anime
{
    public class DetailsApiModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsApiModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync(string? title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title is required.");
            }

            var client = _httpClientFactory.CreateClient("AniList");

            string query = @"
                query ($search: String) {
                    Media(search: $search, type: ANIME) {
                        title {
                            romaji
                        }
                        coverImage {
                            large
                        }
                        description
                        genres
                        averageScore
                    }
                }";

            var requestBody = new
            {
                query,
                variables = new { search = title }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var aniListResponse = JsonSerializer.Deserialize<AniListDetailResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var media = aniListResponse?.Data?.Media;
            if (media == null)
            {
                return new JsonResult(new AnimeItem { Title = "Not Found" });
            }

            var result = new AnimeItem
            {
                Title = media.Title?.Romaji,
                CoverImage = media.CoverImage?.Large,
                Description = media.Description,
                Genres = media.Genres,
                Rating = media.AverageScore
            };

            return new JsonResult(result);
        }
    }

    public class AniListDetailResponse
    {
        public Data? Data { get; set; }
    }

    public class Data
    {
        public Media? Media { get; set; }
    }

    public class Media
    {
        public Title? Title { get; set; }
        public CoverImage? CoverImage { get; set; }
        public string? Description { get; set; }
        public string[]? Genres { get; set; }
        public double AverageScore { get; set; }
    }

    public class Title
    {
        public string? Romaji { get; set; }
    }

    public class CoverImage
    {
        public string? Large { get; set; }
    }
}