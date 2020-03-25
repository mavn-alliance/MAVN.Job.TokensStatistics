using AutoMapper;
using Lykke.Job.TokensStatistics.Client.Models.Responses;
using Lykke.Job.TokensStatistics.Domain.Models;

namespace Lykke.Job.TokensStatistics.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DailyTokensSnapshot, TotalTokensResponse>()
                .ForMember(x => x.ErrorCode, opt => opt.MapFrom(t => t.ErrorCode));

            CreateMap<TokensStatistic, TokensStatisticResponse>();
            CreateMap<TokensStatisticList, TokensStatisticListResponse>();
        }
    }
}
