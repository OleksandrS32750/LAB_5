namespace Task4.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BuildingCode { get; set; }

    public int Floor { get; set; }
    public int Capacity { get; set; }

    public Boolean HasProjector { get; set; }

    public Boolean IsActive { get; set; }
}