using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SecretsController : ControllerBase
{
    private readonly IKeyVaultService _keyVaultService;

    public SecretsController(IKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
    }

    [HttpGet("polygon-api-key")]
    public async Task<IActionResult> GetPolygonApiKey()
    {
        var apiKey = await _keyVaultService.GetSecretAsync("PolygonApiKey");
        
        if (string.IsNullOrEmpty(apiKey))
        {
            return NotFound("API key not found");
        }

        return Ok(new { value = apiKey });
    }
}