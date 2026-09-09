using Microsoft.AspNetCore.Mvc;

namespace Shop.Api.Controllers;

[ApiController]
[Route("test")]
public class TestCTController(ILogger<TestCTController> logger) : ControllerBase
{
    [HttpGet("without")]
    public async Task<IActionResult> TestWithoutCT()
    {
        logger.LogInformation("start test withoutCT");
        await Task.Delay(1000);
        logger.LogInformation("action 1");
        await Task.Delay(2000);
        logger.LogInformation("action 2");
        await Task.Delay(500);

        logger.LogInformation("end test withoutCT");

        return Ok("End");
    }
    [HttpGet("with")]
    public async Task<IActionResult> TestWithCT(CancellationToken token)
    {
        logger.LogInformation("start test withoutCT");
        await Task.Delay(1000, token);
        logger.LogInformation("action 1");
        await Task.Delay(2000, token);
        logger.LogInformation("action 2");
        await Task.Delay(500, token);

        logger.LogInformation("end test withoutCT");

        return Ok("End");
    }
}
