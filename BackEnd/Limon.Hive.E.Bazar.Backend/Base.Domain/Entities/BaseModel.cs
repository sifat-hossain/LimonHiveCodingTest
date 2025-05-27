namespace Base.Domain.Entities;

public class BaseModel
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }
}
