namespace Qi_practice_authentication.Entities;

public class AddUpdateOurHero
{
    
    public required string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public bool isActive { get; set; } = true;
}