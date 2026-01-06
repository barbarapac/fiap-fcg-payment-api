using Fiap.FCG.Payment.WebApi._Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Payment.Unit.Test.WebApi.Shared
{
    public class ExceptionMiddlewareTest
    {
        [Fact]
        public async Task InvokeAsync_QuandoLancaExcecao_DeveRetornarErro500()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();


            var middleware = new ExceptionMiddleware(
            _ => throw new InvalidOperationException("Erro interno"),
            logger.Object);


            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;


            // Act
            await middleware.InvokeAsync(context);


            // Assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            responseStream.Seek(0, SeekOrigin.Begin);
            var responseJson = await new StreamReader(responseStream).ReadToEndAsync();


            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            response.GetProperty("Message").GetString().Should().Be("Erro interno");
            response.GetProperty("StatusCode").GetInt32().Should().Be(500);
            logger.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unhandled exception")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ));
        }
    }
}
