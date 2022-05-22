using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Web.Controllers;

[ApiController]
[Route ("[controller]")]
public class WeatherController : ControllerBase
{
    // GET: /<controller>/
    public IActionResult Get ()
    {
        return Ok ("Es ist mega warm!");
    }
}

