namespace Domain.Models;

public class SequenceModel
{
    public Guid Id { get; set; } 
    public int ObservationCount { get; set; }
    public List<int> Start { get; set; }
    public List<int> Missing { get; set; }
    public string Color { get; set; }
    public SequenceModel(Guid id)
    {
        Id = id;
        Color = "green";
        ObservationCount = 0;
        Missing = new List<int>{0, 0};
        Start = Enumerable.Range(1, 99).ToList();
    }

    public SequenceModel()
    {
    }
}


