using Azure.Messaging.ServiceBus;
using Fiap.FCG.Payment.Application.Compras.Events;
using Fiap.FCG.Payment.Application.Pagamentos.Criar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Fiap.FCG.Payment.Infrastructure.Messaging
{
    public class ComprasRealizadasConsumer : BackgroundService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusOptions _options;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ComprasRealizadasConsumer> _logger;

        private ServiceBusProcessor? _processor;

        public ComprasRealizadasConsumer(ServiceBusClient client, ServiceBusOptions options, IServiceScopeFactory scopeFactory, ILogger<ComprasRealizadasConsumer> logger)
        {
            _client = client;
            _options = options;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var processorOptions = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = true,
                MaxConcurrentCalls = 2
            };

            _processor = _client.CreateProcessor(_options.QueueComprasRealizadas, processorOptions);

            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(cancellationToken);

            _logger.LogInformation("Consumer iniciado para fila {Queue}", _options.QueueComprasRealizadas);

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_processor is not null)
            {
                await _processor.StopProcessingAsync(cancellationToken);
                await _processor.DisposeAsync();
            }

            await base.StopAsync(cancellationToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();

            CompraRealizadaEvent evento;
            try
            {
                evento = JsonSerializer.Deserialize<CompraRealizadaEvent>(body)
                         ?? throw new InvalidOperationException("Mensagem inválida (JSON nulo).");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao desserializar mensagem: {Body}", body);
                throw;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                
                var cmd = new CriarPagamentoCommand
                {
                    CompraId = evento.CompraId,
                    UsuarioId = evento.UsuarioId,
                    ValorTotal = evento.ValorTotal,
                    MetodoPagamento = evento.MetodoPagamento,
                    BandeiraCartao = evento.BandeiraCartao
                };

                var result = await mediator.Send(cmd);

                if (!result.Sucesso)
                {
                    _logger.LogWarning("Pagamento não criado para CompraId={CompraId}. Erro: {Erro}",
                        evento.CompraId, result.Erro);
                    return;
                }

                _logger.LogInformation("Pagamento criado com sucesso para CompraId={CompraId}", evento.CompraId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar pagamento da CompraId={CompraId}", evento.CompraId);
                throw;
            }
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception,
                "Erro no ServiceBusProcessor. Entity={EntityPath}, ErrorSource={ErrorSource}",
                args.EntityPath, args.ErrorSource);

            return Task.CompletedTask;
        }
    }
}
