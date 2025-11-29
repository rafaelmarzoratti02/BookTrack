namespace BookTrack.Core.Entitites;

public class User : BaseEntity
{
    public User(string email, string name) :base()
    {
        Email = email;
        Name = name;
    }

    public string Email { get; set; }
    public string Name { get; set; }
    public List<Review> Reviews { get; set; }
}