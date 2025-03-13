using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Do2Day.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace What2Do2Day.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public List<AnimeItem> AnimeList { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("AniList");

            // GraphQL query to fetch anime (paginated, 50 per page)
            string query = @"
                query ($page: Int, $perPage: Int, $search: String) {
                    Page(page: $page, perPage: $perPage) {
                        media(search: $search, type: ANIME, sort: POPULARITY_DESC) {
                            id
                            title {
                                romaji
                            }
                            coverImage {
                                large
                            }
                        }
                    }
                }";

            var requestBody = new
            {
                query,
                variables = new
                {
                    page = 1,
                    perPage = 50,
                    search = string.IsNullOrEmpty(SearchQuery) ? null : SearchQuery
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var aniListResponse = JsonSerializer.Deserialize<AniListResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            AnimeList = aniListResponse.Data.Page.Media.Select(m => new AnimeItem
            {
                Id = m.Id,
                Title = m.Title.Romaji,
                CoverImage = m.CoverImage.Large
            }).ToList();
        }
    }

    // Helper classes for deserializing AniList response
    public class AniListResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Page Page { get; set; }
    }

    public class Page
    {
        public List<Media> Media { get; set; }
    }

    public class Media
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public CoverImage CoverImage { get; set; }
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