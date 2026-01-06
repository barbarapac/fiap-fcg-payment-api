using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fiap.FCG.Payment.WebApi._Shared;

[ExcludeFromCodeCoverage]
public class RequestLoggingScopeMiddleware
{
    private const string CorrelationHeader = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingScopeMiddleware> _logger;

    public RequestLoggingScopeMiddleware(RequestDelegate next, ILogger<RequestLoggingScopeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var activity = Activity.Current;

        var correlationId =
            context.Request.Headers.TryGetValue(CorrelationHeader, out var headerValue)
            && !string.IsNullOrWhiteSpace(headerValue)
                ? headerValue.ToString()
                : (activity?.TraceId.ToString() ?? context.TraceIdentifier);

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationHeader] = correlationId;
            return Task.CompletedTask;
        });

        using var scope = _logger.BeginScope(new[]
        {
            new KeyValuePair<string, object?>("CorrelationId", correlationId),
            new KeyValuePair<string, object?>("TraceId", activity?.TraceId.ToString()),
            new KeyValuePair<string, object?>("SpanId", activity?.SpanId.ToString()),
            new KeyValuePair<string, object?>("HttpMethod", context.Request.Method),
            new KeyValuePair<string, object?>("HttpPath", context.Request.Path.ToString())
        });

        var sw = Stopwatch.StartNew();
        _logger.LogInformation("Requisi��o HTTP iniciada");

        try
        {
            await _next(context);
            sw.Stop();

            _logger.LogInformation(
                "Requisi��o HTTP finalizada. StatusCode: {StatusCode}. DuracaoMs: {DuracaoMs}",
                context.Response.StatusCode,
                sw.ElapsedMilliseconds);
        }
        catch
        {
            sw.Stop();

            _logger.LogError(
                "Falha ao processar requisi��o HTTP. DuracaoMs: {DuracaoMs}",
                sw.ElapsedMilliseconds);

            throw;
        }
    }
}
