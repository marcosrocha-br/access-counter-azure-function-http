using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AccessCounter.Function;

public class AccessCounter
{
    private readonly ILogger<AccessCounter> _logger;
    private static int _accessCount = 0;

    public AccessCounter(ILogger<AccessCounter> logger)
    {
        _logger = logger;
    }

    [Function("AccessCounter")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        _accessCount++;
        _logger.LogInformation($"Access count: {_accessCount}");
        return new OkObjectResult($"Welcome! You are the visitor number {_accessCount}.");
    }
}