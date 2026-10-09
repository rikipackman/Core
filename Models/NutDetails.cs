namespace core.Models;

public class NutDetails
{
    public int Id {get;set;}
    
    public DateOnly Date { get; set; }

    public string? Name { get; set; }

    public double Weight { get; set; }

    public double Price => (Weight*4/.9) / 100;

}
