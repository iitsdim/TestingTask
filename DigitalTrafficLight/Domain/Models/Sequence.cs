namespace Domain.Models;

public class SequenceModel
{
    public Guid Id { get; } 
    public SequenceModel(Guid id)
    {
        Id = id;
    }
}


