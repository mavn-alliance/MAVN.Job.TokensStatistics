using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using MAVN.Job.TokensStatistics.DomainServices.Subscribers;
using MAVN.Job.TokensStatistics.Settings;
using Lykke.SettingsReader;

namespace MAVN.Job.TokensStatistics.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string QueueName = "tokensstatistics";

        private const string BonusReceivedExchangeName = "lykke.wallet.bonusreceived";
        private const string PaymentTransferSucceededExchangeName = "lykke.wallet.successfulpaymenttransfer";
        private const string PartnersPaymentSucceededExchangeName = "lykke.wallet.successfulpartnerspayment";

        private readonly string _connString;

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _connString = appSettings.CurrentValue.TokensStatisticsJob.RabbitMq.RabbitMqConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BonusReceivedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", BonusReceivedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<PaymentTransferSucceededSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", PaymentTransferSucceededExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<PartnersPaymentSucceededSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", PartnersPaymentSucceededExchangeName)
                .WithParameter("queueName", QueueName);
        }
    }
}
