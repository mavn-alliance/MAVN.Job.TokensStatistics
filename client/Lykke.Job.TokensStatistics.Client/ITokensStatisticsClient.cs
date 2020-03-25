using JetBrains.Annotations;

namespace Lykke.Job.TokensStatistics.Client
{
    /// <summary>
    /// TokensStatistics client interface.
    /// </summary>
    [PublicAPI]
    public interface ITokensStatisticsClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - ITokensStatisticsApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        ITokensStatisticsApi Api { get; }
    }
}
