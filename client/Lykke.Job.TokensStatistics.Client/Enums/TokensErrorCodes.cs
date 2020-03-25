namespace Lykke.Job.TokensStatistics.Client.Enums
{
    /// <summary>
    /// Error codes enum
    /// </summary>
    public enum TokensErrorCodes
    {
        /// <summary>
        /// no error
        /// </summary>
        None,
        /// <summary>
        /// Tokens statistics were not found
        /// </summary>
        StatisticsNotFound,
        /// <summary>
        /// The system was not able to sync total amount value with the one from PBF Service
        /// </summary>
        PrivateBlockchainFacadeIsNotAvailable
    }
}
