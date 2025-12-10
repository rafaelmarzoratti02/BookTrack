namespace BookTrack.Shared.InputModels.Users;

public class LoginInputModel
{
    public LoginInputModel(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}