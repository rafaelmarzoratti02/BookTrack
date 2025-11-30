using BookTrack.Shared.InputModels.Reviews;
using BookTrack.Shared.ViewModels.Reviews;

namespace BookTrack.Application.Services;

public interface IReviewService
{
    Task<int> Insert(CreateReviewInputModel model);
    Task<List<ReviewViewModel>> GetAllByBookId(int bookId);
    Task<ReviewViewModel> GetById(int reviewId);
}