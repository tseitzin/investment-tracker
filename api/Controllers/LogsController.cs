using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly ILogger<LogsController> _logger;

    public LogsController(ILogger<LogsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult LogEntry([FromBody] LogEntry entry)
    {
        var logLevel = entry.Level.ToUpper() switch
        {
            "ERROR" => LogLevel.Error,
            "WARN" => LogLevel.Warning,
            "INFO" => LogLevel.Information,
            _ => LogLevel.Information
        };

        _logger.Log(
            logLevel,
            "Client Log: {Message}. Context: {@Context}. Error: {@Error}",
            entry.Message,
            entry.Context,
            entry.Error
        );

        return Ok();
    }
}

public class LogEntry
{
    public string Level { get; set; } = "info";
    public string Message { get; set; } = "";
    public string? Timestamp { get; set; }
    public object? Context { get; set; }
    public object? Error { get; set; }
}