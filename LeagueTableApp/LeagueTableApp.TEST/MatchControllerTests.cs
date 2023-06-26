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

public partial class MatchControllerTests : IClassFixture
<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _appFactory;
    private readonly Faker<Match> _dtoFaker;
    private readonly JsonSerializerOptions _serializerOptions;

    private List<int> _matchList = new List<int>();

    public MatchControllerTests(CustomWebApplicationFactory appFactory)
    {
        _matchList.Add(101);
        _matchList.Add(102);
        _matchList.Add(103);
        var homeTeamId = getAndRemoveAny(_matchList);
        var foreignTeamId = getAndRemoveAny(_matchList);
        _appFactory = appFactory;
        _dtoFaker = new Faker<Match>()
            .RuleFor(p => p.LeagueId, 101)
            .RuleFor(p => p.HomeTeamId, homeTeamId)
            .RuleFor(p => p.ForeignTeamId, foreignTeamId)
            .RuleFor(p => p.RowVersion, f => f.Random.Bytes(8))
            .RuleFor(p => p.IsEnded, true)
            .RuleFor(p => p.HomeTeamScore, f => f.Random.Int())
            .RuleFor(p => p.ForeignTeamScore, f => f.Random.Int());
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    private int getAndRemoveAny(List<int> original)
    {
        Random random = new Random();
        var index = random.Next(1, original.Count);
        var value = original[index];
        original.RemoveAt(index);
        return value;
    }
}
