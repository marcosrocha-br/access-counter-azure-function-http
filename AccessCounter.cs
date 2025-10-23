using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;

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
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _accessCount++;

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var user = JsonSerializer.Deserialize<User>(requestBody, options);

        _logger.LogInformation($"Request received: {user.ToString()}");
        _logger.LogInformation($"Access count: {_accessCount}");
        return new OkObjectResult($"Welcome {user?.Name}! You are the visitor number {_accessCount}.");
    }
}