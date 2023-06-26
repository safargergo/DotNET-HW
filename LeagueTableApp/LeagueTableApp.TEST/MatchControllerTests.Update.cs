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
        public class Update : MatchControllerTests
        {
            public Update(CustomWebApplicationFactory appFactory)
            : base(appFactory)
            {
            }

            private byte[] RowVersion { get; set; }

            [Fact]
            public async Task Should_Get_Right_Data_Before_Updateing()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();

                // Act
                var response = await client.GetAsync($"/api/Matches/1001");
                var p = await response.Content.ReadFromJsonAsync<Match>(_serializerOptions);

                // Assert                
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                p.LeagueId.Should().Be(101);
                p.HomeTeamId.Should().Be(101);
                p.ForeignTeamId.Should().Be(102);
                p.IsEnded.Should().BeTrue();
                p.HomeTeamScore.Should().Be(2);
                p.ForeignTeamScore.Should().Be(1);
                this.RowVersion = p.RowVersion;
            }

            [Fact]
            public async Task Should_Get_Right_Data_After_Updateing()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();
                dto.RowVersion = this.RowVersion;

                // Act
                var elotte = await client.GetAsync($"/api/Matches/1001");
                var elotteMatch = await elotte.Content.ReadFromJsonAsync<Match>(_serializerOptions);
                dto.RowVersion = elotteMatch.RowVersion;
                var response = await client.PutAsJsonAsync($"/api/Matches/1001", dto, _serializerOptions);
                var response2 = await client.GetAsync($"/api/Matches/1001");
                var p = await response2.Content.ReadFromJsonAsync<Match>(_serializerOptions);

                // Assert
                p.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response2.StatusCode.Should().Be(HttpStatusCode.OK);
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
