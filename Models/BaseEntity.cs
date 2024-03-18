namespace Qi_practice_authentication.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }  = DateTime.Now;
}