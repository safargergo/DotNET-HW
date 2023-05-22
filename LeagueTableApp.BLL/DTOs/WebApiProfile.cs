using AutoMapper;

namespace LeagueTableApp.BLL.DTOs;

public class WebApiProfile : Profile
{
    public WebApiProfile()
    {
        CreateMap<DAL.Entities.League, League>().ReverseMap();
        CreateMap<DAL.Entities.Match, Match>().ReverseMap();
        CreateMap<DAL.Entities.Team, Team>().ReverseMap();
    }
}
