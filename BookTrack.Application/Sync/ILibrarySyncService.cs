namespace BookTrack.Application.Sync;

public interface ILibrarySyncService
{
    Task<List<LibraryBookDto>> GetBooksFromLibraryAsync(CancellationToken cancellationToken = default);
}
