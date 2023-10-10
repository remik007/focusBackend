using System.Diagnostics;

namespace FocusAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            if(elapsedMiliseconds / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
