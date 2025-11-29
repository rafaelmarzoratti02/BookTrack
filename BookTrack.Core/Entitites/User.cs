namespace BookTrack.Core.Entitites;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public List<Review> Reviews { get; set; }
}