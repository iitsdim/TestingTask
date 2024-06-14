using Contracts.Sequence;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controller;

[ApiController]
public class SequenceController : ControllerBase
{
    [HttpPost("/sequence")]
    public IActionResult CreateSequence(CreateSequenceRequest request)
    {
        return Ok(request);
    }
    [HttpGet("/sequence/{id:guid}")]
    public IActionResult GetSequence(Guid id)
    {
       return Ok(id);
    }
}