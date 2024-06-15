namespace Domain.Models;

public class SequenceModel
{
    public Guid Id { get; } 
    public int ObservationCount { get; set; }
    public List<int> Start { get; set; }
    public List<string> Missing { get; set; }
    public string Color { get; set; }
    public SequenceModel(Guid id)
    {
        Id = id;
        Color = "green";
        ObservationCount = 0;
        Missing = new List<string>{ "0000000", "0000000"};
        Start = Enumerable.Range(1, 99).ToList();
    }
}


