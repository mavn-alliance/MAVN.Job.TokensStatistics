using AutoMapper;
using MAVN.Job.TokensStatistics.Client.Models.Responses;
using MAVN.Job.TokensStatistics.Domain.Models;

namespace MAVN.Job.TokensStatistics.MappingProfiles
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
