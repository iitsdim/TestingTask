using Microsoft.AspNetCore.Mvc;

namespace Domain.Controller;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {
        return Problem();
    }
}