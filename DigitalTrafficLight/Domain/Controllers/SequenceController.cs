using Contracts.Observation;
using Contracts.Sequence;
using Domain.Models;
using Domain.Services.Sequence;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controller;

[ApiController]
public class SequenceController : ControllerBase
{
    private readonly ISequenceService _sequenceService;

    public SequenceController(ISequenceService sequenceService) {
        _sequenceService = sequenceService;
    }

    [HttpPost("sequence/create")]
    public IActionResult CreateSequence(CreateSequenceRequest request)
    {
        var sequence = new SequenceModel(
            Guid.NewGuid()
        );
        _sequenceService.CreateSequence(sequence);
        var response = new SequenceResponse(sequence.Id);

        return CreatedAtAction(
            actionName: nameof(GetSequence),
            routeValues: new { id=sequence.Id },
            value: response
        );
    }
    [HttpGet("/sequence/{id:guid}")]
    public IActionResult GetSequence(Guid id)
    {
        SequenceModel sequence = _sequenceService.GetSequence(id);
        var response = new SequenceResponse(sequence.Id);
       return Ok(response);
    }

    [HttpPost("observation/add")]
    public IActionResult AddObservation(ObservationRequest request)
    {
        try
        {
            string sequenceId = request.Sequence;
            Observation observation = request.Observation;
            var response = new ObservationResponse([], [sequenceId, observation.Color]);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Msg = ex.Message });
        }   
    }

    [HttpGet("/clear")]
    public IActionResult Clear()
    {
        _sequenceService.Clear();
        return Ok(new {response = "ok"});
    }
}