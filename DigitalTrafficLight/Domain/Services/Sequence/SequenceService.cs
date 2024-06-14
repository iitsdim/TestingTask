using Domain.Models;
namespace Domain.Services.Sequence;

public class SequenceService : ISequenceService 
{
    private static readonly Dictionary<Guid, SequenceModel> _sequenceMap = new();

    public void Clear()
    {
        _sequenceMap.Clear();
    }

    public void CreateSequence(SequenceModel sequence) 
    {
        _sequenceMap.Add(sequence.Id, sequence);
    }

    public SequenceModel GetSequence(Guid id)
    {
        return _sequenceMap[id];
    }
}