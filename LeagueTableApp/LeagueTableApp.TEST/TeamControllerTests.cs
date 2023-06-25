using Bogus;
using LeagueTableApp.API.Controllers;
using LeagueTableApp.BLL.DTOs;
using LeagueTableApp.BLL.Services;
using LeagueTableApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace LeagueTableApp.TEST;

public partial class TeamControllerTests : IClassFixture
<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _appFactory;
    private readonly Faker<Team> _dtoFaker;
    private readonly JsonSerializerOptions _serializerOptions;

    public TeamControllerTests(CustomWebApplicationFactory appFactory)
    {
        _appFactory = appFactory;
        _dtoFaker = new Faker<Team>()
        //.RuleFor(p => p.Id, 55)
        .RuleFor(p => p.Name, f => f.Random.Word())
        .RuleFor(p => p.Coach, f => f.Random.Word())
        .RuleFor(p => p.Players, f => f.Random.String2(200))
        .RuleFor(p => p.LeagueId, f => f.Random.Int(101, 102));
        //.RuleFor(p => p.RowVersion, f => f.Random.Bytes(5));

        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}

