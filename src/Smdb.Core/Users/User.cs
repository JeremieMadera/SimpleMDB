namespace Smdb.Core.Users;

public class User
{
    public int Id { get; set; }

    // Inicializamos con string.Empty para evitar errores de nulabilidad
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Constructor para el Seed en MemoryDatabase
    public User(int id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }

    // Constructor vacío necesario para que la API reciba datos (POST/PUT)
    public User() { }
}