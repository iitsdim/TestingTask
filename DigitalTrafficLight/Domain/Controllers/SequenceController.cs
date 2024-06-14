using Contracts.Sequence;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controller;

[ApiController]
[Route("[controller]")]
public class SequenceController : ControllerBase
{
    [HttpPost()]
    public IActionResult CreateSequence(CreateSequenceRequest request)
    {
        var sequence = new Sequence(
            Guid.NewGuid()
        );

        var response = new SequenceResponse(
            sequence.Id
        );

        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetSequence(Guid id)
    {
       return Ok(id);
    }
}