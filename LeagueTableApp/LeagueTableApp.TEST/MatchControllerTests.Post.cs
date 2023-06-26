using FluentAssertions;
using LeagueTableApp.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LeagueTableApp.TEST
{
    public partial class MatchControllerTests
    {
        public class Post : MatchControllerTests
        {
            public Post(CustomWebApplicationFactory appFactory)
            : base(appFactory)
            {
            }

            [Fact]
            public async Task Should_Succeded_With_Created()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();

                // Act
                var response = await client.PostAsJsonAsync($"/api/Matches", dto, _serializerOptions);
                var p = await response.Content.ReadFromJsonAsync<Match>(_serializerOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                response.Headers.Location
                  .Should().Be(
                  new Uri(_appFactory.Server.BaseAddress, $"/api/Matches/{p.Id}")
                  );
                p.Should().BeEquivalentTo(
                  dto,
                  opt => opt.Excluding(x => x.Id)
                            .Excluding(x => x.HomeTeam)
                            .Excluding(x => x.ForeignTeam)
                            .Excluding(x => x.League)
                            .Excluding(x => x.RowVersion));
                p.LeagueId.Should().Be(dto.LeagueId);
                p.HomeTeamId.Should().Be(dto.HomeTeamId);
                p.ForeignTeamId.Should().Be(dto.ForeignTeamId);
                p.IsEnded.Should().BeTrue();
                p.HomeTeamScore.Should().Be(dto.HomeTeamScore);
                p.ForeignTeamScore.Should().Be(dto.ForeignTeamScore);
            }
        }
    }
}
