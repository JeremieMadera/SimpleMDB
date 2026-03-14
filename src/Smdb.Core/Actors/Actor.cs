namespace Smdb.Core.Actors;

public class Actor
{
    public int Id { get; set; }

    // Al añadir = string.Empty; evitas el error CS8618
    public string Name { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    // Constructor para el Seed
    public Actor(int id, string name, DateTime birthDate)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
    }

    // Constructor vacío para el sistema (JSON)
    public Actor() { }
}