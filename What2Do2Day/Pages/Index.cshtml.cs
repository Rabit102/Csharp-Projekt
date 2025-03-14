using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Do2Day.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace What2Do2Day.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; } // Nullable gemacht

        public List<AnimeItem> AnimeList { get; set; } = new();
        public List<string> Categories { get; set; } = new List<string> { "Am Schauen" };
        public Dictionary<string, List<AnimeItem>> CategorizedAnime { get; set; } = new();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("AniList");

            string query = @"
                query ($page: Int, $perPage: Int, $search: String) {
                    Page(page: $page, perPage: $perPage) {
                        media(search: $search, type: ANIME, sort: POPULARITY_DESC) {
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
                    search = SearchQuery
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var aniListResponse = JsonSerializer.Deserialize<AniListResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                AnimeList = aniListResponse?.Data?.Page?.Media?.Select(m => new AnimeItem
                {
                    Title = m.Title?.Romaji,
                    CoverImage = m.CoverImage?.Large,
                    Category = "Am Schauen"
                }).ToList() ?? new List<AnimeItem>();

                CategorizedAnime = AnimeList
                    .Where(a => a.Category != null) // Nur Einträge mit Kategorie
                    .GroupBy(a => a.Category!)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
            else
            {
                AnimeList = new List<AnimeItem>();
            }
        }

        public IActionResult OnPostAddCategory(string? newCategory)
        {
            if (!string.IsNullOrEmpty(newCategory) && !Categories.Contains(newCategory))
            {
                Categories.Add(newCategory);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostAddAnimeToCategory(string? animeTitle, string? category)
        {
            var anime = AnimeList.FirstOrDefault(a => a.Title == animeTitle);
            if (anime != null && !string.IsNullOrEmpty(category) && Categories.Contains(category))
            {
                anime.Category = category;
                CategorizedAnime = AnimeList
                    .Where(a => a.Category != null)
                    .GroupBy(a => a.Category!)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
            return RedirectToPage();
        }
    }

    public class AniListResponse
    {
        public Data? Data { get; set; }
    }

    public class Data
    {
        public Page? Page { get; set; }
    }

    public class Page
    {
        public List<Media>? Media { get; set; }
    }

    public class Media
    {
        public Title? Title { get; set; }
        public CoverImage? CoverImage { get; set; }
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