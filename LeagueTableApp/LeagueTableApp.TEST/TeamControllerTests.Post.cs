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
        public class Post : TeamControllerTests
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
                var response = await client.PostAsJsonAsync($"/api/Teams", dto, _serializerOptions);
                var p = await response.Content.ReadFromJsonAsync<Team>(_serializerOptions);
                //var response2 = await client.GetAsync($"/api/Teams");
                //p = await client.GetFromJsonAsync<Team>($"/api/Teams/104", _serializerOptions);

                // Assert
                //p.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                response.Headers.Location
                  .Should().Be(
                  new Uri(_appFactory.Server.BaseAddress, $"/api/Teams/{p.Id}")
                  );
                p.Should().BeEquivalentTo(
                  dto,
                  opt => opt.Excluding(x => x.Id)
                  /*.Excluding(x => x.Name)
                  .Excluding(x => x.Coach)
                  .Excluding(x => x.Captain)
                  .Excluding(x => x.Players)
                  .Excluding(x => x.LeagueId)*/);
                //p.Id.Should().BePositive();
                p.Name.Should().Be(dto.Name);
                p.Coach.Should().Be(dto.Coach);
                p.Captain.Should().BeNull();
                p.Players.Should().Be(dto.Players);
                p.LeagueId.Should().Be(dto.LeagueId);
                /*p.Category.Id.Should().Be(dto.CategoryId);
                p.Orders.Should().BeEmpty();
                p.RowVersion.Should().NotBeEmpty();*/
            }
        }
    }
}
