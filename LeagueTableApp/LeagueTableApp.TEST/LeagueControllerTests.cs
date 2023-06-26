using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using LeagueTableApp.BLL.DTOs;

namespace LeagueTableApp.TEST;

public partial class LeagueControllerTests : IClassFixture
<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _appFactory;
    private readonly Faker<League> _dtoFaker;
    private readonly JsonSerializerOptions _serializerOptions;

    public LeagueControllerTests(CustomWebApplicationFactory appFactory)
    {
        _appFactory = appFactory;
        _dtoFaker = new Faker<League>()
            .RuleFor(p => p.Name, f => f.Random.Words())
            .RuleFor(p => p.Description, f => f.Random.String2(250));

        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}
