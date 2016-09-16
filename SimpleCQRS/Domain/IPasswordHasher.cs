namespace SimpleCQRS.Domain
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
