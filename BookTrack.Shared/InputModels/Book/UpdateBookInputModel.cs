using BookTrack.Core.Enums;

namespace BookTrack.Shared.InputModels;

public class UpdateBookInputModel
{
    public int IdBook { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearOfPublication { get; set; }

}