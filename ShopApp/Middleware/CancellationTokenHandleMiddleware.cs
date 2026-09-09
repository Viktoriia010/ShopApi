namespace Shop.Api.Middleware;

public class CancellationTokenHandleMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CancellationTokenHandleMiddleware> _logger;
    public CancellationTokenHandleMiddleware(RequestDelegate next, ILogger<CancellationTokenHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch (TaskCanceledException ex) 
        {
            _logger.LogError(ex.Message);
        }
        
    }
}
    