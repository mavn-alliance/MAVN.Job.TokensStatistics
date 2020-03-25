using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.TokensStatistics.Domain.Services;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Job.TokensStatistics.DomainServices.Subscribers
{
    public class PartnersPaymentSucceededSubscriber : JsonRabbitSubscriber<SuccessfulPartnersPaymentEvent>
    {
        private readonly ITokensStatisticsService _tokensStatisticsService;
        private readonly ILog _log;

        public PartnersPaymentSucceededSubscriber(
            ITokensStatisticsService tokensStatisticsService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _tokensStatisticsService = tokensStatisticsService;
            _log = logFactory.CreateLog(this);
        }

        protected override Task ProcessMessageAsync(SuccessfulPartnersPaymentEvent message)
        {
            if (message.Amount == 0)
            {
                _log.Warning("Partners payment received event with 0 amount", context: message);
            }
            else
            {
                _tokensStatisticsService.IncreaseBurnedAmount(message.Amount);
                _log.Info("Processed SuccessfulPartnersPaymentEvent", message);
            }

            return Task.CompletedTask;
        }
    }
}
