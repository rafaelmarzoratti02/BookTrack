namespace BookTrack.Core.Exceptions;

public class IsbnAlreadyExistsException : DomainException
{
    public IsbnAlreadyExistsException()
        : base("Book already exists with this ISBN") { }
}