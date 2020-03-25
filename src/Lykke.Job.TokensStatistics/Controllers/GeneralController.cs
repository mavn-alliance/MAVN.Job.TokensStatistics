using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Job.TokensStatistics.Client;
using Lykke.Job.TokensStatistics.Client.Enums;
using Lykke.Job.TokensStatistics.Client.Models.Requests;
using Lykke.Job.TokensStatistics.Client.Models.Responses;
using Lykke.Job.TokensStatistics.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Job.TokensStatistics.Controllers
{
    [Route("api/general")]
    public class GeneralController : ControllerBase, ITokensStatisticsApi
    {
        private readonly ITokensStatisticsService _tokensStatisticsService;
        private readonly IMapper _mapper;

        public GeneralController(ITokensStatisticsService tokensStatisticsService, IMapper mapper)
        {
            _tokensStatisticsService = tokensStatisticsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets total amount of tokens in the system
        /// </summary>
        /// <returns><see cref="TotalTokensResponse"/></returns>
        [HttpGet("total")]
        [ProducesResponseType(typeof(TotalTokensResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<TotalTokensResponse> GetTotalAmountAsync
            ([Required][FromQuery] DateTime dt)
        {
            var result = await _tokensStatisticsService.GetTokensSnapshotForDate(dt);

            return _mapper.Map<TotalTokensResponse>(result);
        }

        /// <summary>
        /// Get tokens statistics for period grouped by days
        /// </summary>
        /// <param name="request">The period details</param>
        /// <returns></returns>
        [HttpGet("byDays")]
        [ProducesResponseType(typeof(TokensStatisticListResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<TokensStatisticListResponse> GetByDaysAsync([FromQuery] PeriodRequest request)
        {
            var result = await _tokensStatisticsService.GetTokensStatsForPeriod(request.FromDate, request.ToDate);

            return _mapper.Map<TokensStatisticListResponse>(result);
        }

        /// <summary>
        /// Syncs the total tokens amount value with the one from PBF
        /// This should be used in case there is a big deviation between the two values
        /// </summary>
        /// <returns><see cref="SyncTotalTokensResponse"/></returns>
        [HttpGet("sync")]
        [ProducesResponseType(typeof(SyncTotalTokensResponse), (int)HttpStatusCode.NoContent)]
        public async Task<SyncTotalTokensResponse> SyncTotalTokensAsync()
        {
            var result = await _tokensStatisticsService.SyncTotalTokensAsync();

            return new SyncTotalTokensResponse { ErrorCode = (TokensErrorCodes)result};
        }

        /// <summary>
        /// Gets in-memory total amount of tokens from tokens statistics 
        /// </summary>
        /// <returns><see cref="TotalTokensResponse"/></returns>
        [HttpGet("total/current")]
        [ProducesResponseType(typeof(TotalTokensResponse), (int)HttpStatusCode.OK)]
        public Task<TotalTokensResponse> GetCurrentTotalAmountAsync()
        {
            var result =  _tokensStatisticsService.GetCurrentTotalAmountAsync();

            return Task.FromResult(new TotalTokensResponse{TotalTokensAmount = result});
        }
    }
}
