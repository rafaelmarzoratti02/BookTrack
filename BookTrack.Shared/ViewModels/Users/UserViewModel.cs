using BookTrack.Core.Entitites;
using BookTrack.Shared.ViewModels.Reviews;

namespace BookTrack.Shared.ViewModels.Users;

public class UserViewModel
{
    public UserViewModel(string email, string name, List<ReviewItemViewModel> reviews)
    {
        Email = email;
        Name = name;
        Reviews = reviews;
    }

    public string Email { get; set; }
    public string Name { get; set; }
    public List<ReviewItemViewModel> Reviews { get; set; }
    
    public static UserViewModel FromEntity(User user)
    { 
        List<ReviewItemViewModel> reviews;
        reviews = user.Reviews.Select(x => ReviewItemViewModel.FromEntity(x)).ToList();
        
        return new UserViewModel(user.Email, user.Name, reviews);
    }
}