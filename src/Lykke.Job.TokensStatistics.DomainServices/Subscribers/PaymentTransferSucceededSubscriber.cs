using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.TokensStatistics.Domain.Services;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Job.TokensStatistics.DomainServices.Subscribers
{
    public class PaymentTransferSucceededSubscriber : JsonRabbitSubscriber<SuccessfulPaymentTransferEvent>
    {
        private readonly ITokensStatisticsService _tokensStatisticsService;
        private readonly ILog _log;

        public PaymentTransferSucceededSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ITokensStatisticsService tokensStatisticsService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _tokensStatisticsService = tokensStatisticsService;
            _log = logFactory.CreateLog(this);
        }

        protected override Task ProcessMessageAsync(SuccessfulPaymentTransferEvent message)
        {
            if (message.Amount == 0)
            {
                _log.Warning("Payment transfer received event with 0 amount", context: message);
            }
            else
            {
                // todo: there is a chance to get event raised before midnight but received only after midnight
                _tokensStatisticsService.IncreaseBurnedAmount(message.Amount);
                _log.Info($"Processed payment transfer event", message);
            }

            return Task.CompletedTask;
        }
    }
}
