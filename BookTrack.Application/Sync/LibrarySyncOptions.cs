namespace BookTrack.Application.Sync;

public class LibrarySyncOptions
{
    public string ApiBaseUrl { get; set; }
    public string ApiKey { get; set; }
    public int PollingIntervalInSeconds { get; set; }
}
