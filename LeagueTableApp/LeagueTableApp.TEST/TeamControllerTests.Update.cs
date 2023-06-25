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
    public partial class TeamControllerTests
    {
        public class Update : TeamControllerTests
        {
            public Update(CustomWebApplicationFactory appFactory)
            : base(appFactory)
            {
            }

            [Fact]
            public async Task Should_Get_Right_Data_Before_Updateing()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();

                // Act
                var response = await client.GetAsync($"/api/Teams/105");
                var p = await response.Content.ReadFromJsonAsync<Team>(_serializerOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                p.Name.Should().Be("TestTeamForUpdate");
                p.Coach.Should().Be("Csercsaszov");
                p.Captain.Should().Be("Aladár");
                p.Players.Should().Be("Aladár, Béla, Csanád, Dániel");
                p.LeagueId.Should().Be(102);
            }

            [Fact]
            public async Task Should_Get_Right_Data_After_Updateing()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();

                // Act
                var response = await client.PutAsJsonAsync($"/api/Teams/105", dto, _serializerOptions);
                var response2 = await client.GetAsync($"/api/Teams/105");
                //var p = await response.Content.ReadFromJsonAsync<Team>(_serializerOptions);
                var p = await response2.Content.ReadFromJsonAsync<Team>(_serializerOptions);

                // Assert
                p.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response2.StatusCode.Should().Be(HttpStatusCode.OK);
                p.Should().BeEquivalentTo(
                  dto,
                  opt => opt.Excluding(x => x.Id)
                        .Excluding(x => x.Name)
                        .Excluding(x => x.LeagueId));
                //p.Name.Should().Be(dto.Name);
                p.Coach.Should().Be(dto.Coach);
                p.Captain.Should().BeNull();
                p.Players.Should().Be(dto.Players);
                //p.LeagueId.Should().Be(dto.LeagueId);
            }

        }
    }
}
