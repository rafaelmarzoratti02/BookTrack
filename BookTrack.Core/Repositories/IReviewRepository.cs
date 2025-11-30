using BookTrack.Core.Entitites;

namespace BookTrack.Core.Repositories;

public interface IReviewRepository
{
    Task<List<Review>> GetAllByBookId(int bookId);
    Task<Review> GetById(int id);
    Task<int> Add(Review review);
    Task UpdateBookAverageRating(int bookId);
    Task<bool> ReviewAlreadyExists(int bookId,int userId);
}