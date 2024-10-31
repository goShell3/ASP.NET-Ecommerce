public class User{
    public User(Guid guid, string firstName, string lastName, string email, string password)
    {
        Guid = guid;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public Guid Guid { get; }
}