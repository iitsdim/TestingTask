using Contracts.Observation;
using Domain.Models;

namespace Domain.Services.Sequence;

public interface ISequenceService
{
    void CreateSequence(SequenceModel sequence);
    SequenceModel GetSequence(Guid id);
    void Clear();
    ObservationResponse AddObservation(Guid Id, Observation observation);
}