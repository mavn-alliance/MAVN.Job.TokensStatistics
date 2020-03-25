using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.TokensStatistics.Domain.Services;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Job.TokensStatistics.DomainServices.Subscribers
{
    public class BonusReceivedSubscriber : JsonRabbitSubscriber<BonusReceivedEvent>
    {
        private readonly ITokensStatisticsService _tokensStatisticsService;
        private readonly ILog _log;

        public BonusReceivedSubscriber(
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

        protected override Task ProcessMessageAsync(BonusReceivedEvent message)
        {
            if (message.Amount == 0)
            {
                _log.Warning("Bonus received event with 0 amount", context: message);
            }
            else
            {
                // todo: there is a chance to get event raised before midnight but received only after midnight
                _tokensStatisticsService.IncreaseEarnedAmount(message.Amount);
                _log.Info($"Processed bonus received event", message);
            }

            return Task.CompletedTask;
        }
    }
}
