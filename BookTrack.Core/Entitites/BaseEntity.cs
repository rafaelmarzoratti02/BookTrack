namespace BookTrack.Core.Entitites;

public class BaseEntity
{
    public BaseEntity()
    {
        IsActive = true;
        CreatedOn = DateTime.Now;
    }

    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }

    public void SetAsDeleted()
    {
        IsActive = false;
    }
}   