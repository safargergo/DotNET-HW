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
        public class Post : LeagueControllerTests
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
                var response = await client.PostAsJsonAsync($"/api/Leagues", dto, _serializerOptions);
                var p = await response.Content.ReadFromJsonAsync<League>(_serializerOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                response.Headers.Location
                  .Should().Be(
                  new Uri(_appFactory.Server.BaseAddress, $"/api/Leagues/{p.Id}")
                  );
                p.Should().BeEquivalentTo(
                  dto,
                  opt => opt.Excluding(x => x.Id));
                p.Name.Should().Be(dto.Name);
                p.Description.Should().Be(dto.Description);
            }
        }
    }
}
