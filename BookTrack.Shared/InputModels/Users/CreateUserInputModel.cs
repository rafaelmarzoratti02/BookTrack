using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels.Users;

public class CreateUserInputModel 
{
    public string Email { get; set; }
    public string Name { get; set; }
    
    public User ToEntity()
        => new(Email,Name);

}