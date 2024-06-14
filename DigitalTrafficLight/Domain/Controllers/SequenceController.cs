using Contracts.Sequence;
using Domain.Models;
using Domain.Services.Sequence;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controller;

[ApiController]
[Route("[controller]")]
public class SequenceController : ControllerBase
{
    private readonly ISequenceService _sequenceService;

    public SequenceController(ISequenceService sequenceService) {
        _sequenceService = sequenceService;
    }

    [HttpPost()]
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
    [HttpGet("{id:guid}")]
    public IActionResult GetSequence(Guid id)
    {
        SequenceModel sequence = _sequenceService.GetSequence(id);
        var response = new SequenceResponse(sequence.Id);
       return Ok(response);
    }
}