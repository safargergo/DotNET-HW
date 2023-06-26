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
    public partial class LeagueControllerTests
    {
        public class Update : LeagueControllerTests
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
                var response = await client.GetAsync($"/api/Leagues/101");
                var p = await response.Content.ReadFromJsonAsync<League>(_serializerOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                p.Name.Should().Be("TestLeague1");
                p.Description.Should().Be("It's a league for testing, for example test changeing values or deleting.");
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
                var response = await client.PutAsJsonAsync($"/api/Leagues/101", dto, _serializerOptions);
                var response2 = await client.GetAsync($"/api/Leagues/101");
                var p = await response2.Content.ReadFromJsonAsync<League>(_serializerOptions);

                // Assert
                p.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response2.StatusCode.Should().Be(HttpStatusCode.OK);
                p.Should().BeEquivalentTo(
                  dto,
                  opt => opt.Excluding(x => x.Id)
                        .Excluding(x => x.Name));
                p.Description.Should().Be(dto.Description);
            }

        }
    }
}
