using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels.Users;

public class CreateUserInputModel 
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    public User ToEntity(string passwordHash)
        => new(Email,Name,passwordHash,Role);

}