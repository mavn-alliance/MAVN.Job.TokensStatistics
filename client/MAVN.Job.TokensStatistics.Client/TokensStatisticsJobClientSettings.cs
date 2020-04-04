using Lykke.SettingsReader.Attributes;

namespace MAVN.Job.TokensStatistics.Client 
{
    /// <summary>
    /// TokensStatistics client settings.
    /// </summary>
    public class TokensStatisticsJobClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
