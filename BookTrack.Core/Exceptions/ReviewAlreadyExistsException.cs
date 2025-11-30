namespace BookTrack.Core.Exceptions;

public class ReviewAlreadyExistsException : DomainException
{
    public ReviewAlreadyExistsException() 
        :base("Review already exists with this userId for this bookId") { }
}