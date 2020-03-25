using Lykke.HttpClientGenerator;

namespace Lykke.Job.TokensStatistics.Client
{
    /// <summary>
    /// TokensStatistics API aggregating interface.
    /// </summary>
    public class TokensStatisticsClient : ITokensStatisticsClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to TokensStatistics Api.</summary>
        public ITokensStatisticsApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public TokensStatisticsClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<ITokensStatisticsApi>();
        }
    }
}
