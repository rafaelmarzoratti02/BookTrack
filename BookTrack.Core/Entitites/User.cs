namespace BookTrack.Core.Entitites;

public class User : BaseEntity
{
    public User(string email, string name, string password, string role) : base()
    {
        Email = email;
        Name = name;
        Password = password;
        Role = role;
        Reviews = [];
    }

    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public List<Review> Reviews { get; set; }
    
    public void Update(string name)
    {
        Name = name;
    }
}