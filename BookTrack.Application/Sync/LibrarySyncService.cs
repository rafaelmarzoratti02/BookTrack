using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BookTrack.Application.Sync;

public class LibrarySyncService : ILibrarySyncService
{
    private readonly HttpClient _httpClient;
    private readonly LibrarySyncOptions _options;
    private readonly ILogger<LibrarySyncService> _logger;

    public LibrarySyncService(
        HttpClient httpClient,
        IOptions<LibrarySyncOptions> options,
        ILogger<LibrarySyncService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;

        _httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _options.ApiKey);
    }

    public async Task<List<LibraryBookDto>> GetBooksFromLibraryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Fetching books from Library API at {BaseUrl}", _options.ApiBaseUrl);

            var response = await _httpClient.GetAsync("/api/books/sync", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch books from Library API. Status: {StatusCode}", response.StatusCode);
                return new List<LibraryBookDto>();
            }

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var books = JsonSerializer.Deserialize<List<LibraryBookDto>>(jsonContent, jsonOptions);

            _logger.LogInformation("Successfully fetched {Count} books from Library API", books?.Count ?? 0);

            return books ?? new List<LibraryBookDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching books from Library API");
            return new List<LibraryBookDto>();
        }
    }
}
