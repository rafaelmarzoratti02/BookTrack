namespace BookTrack.Core.Entitites;

public class Review : BaseEntity
{
    public int Rating { get; set; }
    public string Description { get; set; }
    public int IdUsuario { get; set; }
    public int IdLivro { get; set; }
}