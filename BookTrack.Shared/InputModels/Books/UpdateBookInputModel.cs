namespace BookTrack.Shared.InputModels.Books;

public class UpdateBookInputModel
{
    public int IdBook { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearOfPublication { get; set; }

}