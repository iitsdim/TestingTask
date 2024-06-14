namespace Contracts.Observation;

public record ObservationResponse(
    List<int> Start,
    List<string> Missing
);