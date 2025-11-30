using BookTrack.Core.Entitites;

namespace BookTrack.Shared.InputModels.Users;

public class UpdateUserInputModel
{
    public int IdUser { get; set; }
    public string Name { get; set; }
}