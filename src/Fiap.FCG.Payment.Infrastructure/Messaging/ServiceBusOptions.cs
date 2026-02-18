namespace Fiap.FCG.Payment.Infrastructure.Messaging
{
    public class ServiceBusOptions
    {
        public string ConnectionString { get; set; } = default!;
        public string QueueComprasRealizadas { get; set; } = "compras-realizadas";
    }
}
