namespace Domain.Entities;

public class SequenceEntity
{
    public Guid Id { get; set; } 
    public int ObservationCount { get; set; } = 0;
    public List<int> Start { get; set; } = new List<int>();
    public List<int> Missing { get; set; } = new List<int>();
    public string Color { get; set; } = string.Empty;
}


