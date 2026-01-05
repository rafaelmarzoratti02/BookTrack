using BookTrack.Application.Sync;
using BookTrack.Core.Entitites;
using BookTrack.Core.Enums;
using BookTrack.Core.Repositories;
using Microsoft.Extensions.Options;

namespace BookTrack.API.BackgroundServices;

public class BookSyncHostedService : BackgroundService
{
    private readonly ILogger<BookSyncHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly LibrarySyncOptions _options;

    public BookSyncHostedService(
        ILogger<BookSyncHostedService> logger,
        IServiceProvider serviceProvider,
        IOptions<LibrarySyncOptions> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Book Sync Hosted Service is starting");

        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Starting book synchronization at {Time}", DateTimeOffset.UtcNow);

                await SyncBooksAsync(stoppingToken);

                _logger.LogInformation("Book synchronization completed. Next sync in {Interval} seconds",
                    _options.PollingIntervalInSeconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during book synchronization");
            }

            await Task.Delay(TimeSpan.FromSeconds(_options.PollingIntervalInSeconds), stoppingToken);
        }

        _logger.LogInformation("Book Sync Hosted Service is stopping");
    }

    private async Task SyncBooksAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var librarySyncService = scope.ServiceProvider.GetRequiredService<ILibrarySyncService>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var libraryBooks = await librarySyncService.GetBooksFromLibraryAsync(cancellationToken);

        if (libraryBooks == null || !libraryBooks.Any())
        {
            _logger.LogInformation("No books found from Library API");
            return;
        }

        _logger.LogInformation("Fetched {Count} books from Library API", libraryBooks.Count);

        int addedCount = 0;
        int skippedCount = 0;

        foreach (var libraryBook in libraryBooks)
        {
            try
            {
                var exists = await unitOfWork.Books.IsbnExists(libraryBook.ISBN);

                if (exists)
                {
                    skippedCount++;
                    continue;
                }

                var book = new Book(
                    title: libraryBook.Title,
                    description: $"Synced from Library - Author: {libraryBook.Autor}",
                    isbn: libraryBook.ISBN,
                    author: libraryBook.Autor,
                    genre: BookGenreEnum.Fiction,
                    yearOfPublication: libraryBook.AnoDePublicacao,
                    numberOfPages: 0
                );

                await unitOfWork.Books.Add(book);
                await unitOfWork.CompleteAsync();
                addedCount++;

                _logger.LogInformation("Added new book: {Title} (ISBN: {ISBN})", book.Title, book.Isbn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing book with ISBN {ISBN}", libraryBook.ISBN);
            }
        }

        _logger.LogInformation("Sync completed. Added: {Added}, Skipped: {Skipped}", addedCount, skippedCount);
    }
}
