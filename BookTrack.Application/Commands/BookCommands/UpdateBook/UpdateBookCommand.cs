using MediatR;

namespace BookTrack.Application.Commands.BookCommands.UpdateBook;

public class UpdateBookCommand : IRequest
{
    public int IdBook { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearOfPublication { get; set; }
}