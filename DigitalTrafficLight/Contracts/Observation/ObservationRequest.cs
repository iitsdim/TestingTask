namespace Contracts.Observation;

public record Observation(
    string Color, 
    List<string>? Numbers);
public record ObservationRequest(
    Observation Observation,
    Guid Sequence);
