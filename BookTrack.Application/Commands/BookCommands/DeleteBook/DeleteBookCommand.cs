using MediatR;

namespace BookTrack.Application.Commands.BookCommands.DeleteBook;

public class DeleteBookCommand : IRequest
{
    public DeleteBookCommand(int bookId)
    {
        BookId = bookId;
    }

    public int BookId { get; set; }
}