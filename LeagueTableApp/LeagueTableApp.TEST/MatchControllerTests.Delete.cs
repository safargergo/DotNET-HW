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
        public class Delete : MatchControllerTests
        {
            public Delete(CustomWebApplicationFactory appFactory)
            : base(appFactory)
            {
            }


            [Fact]
            public async Task Should_Get_Right_Data_Before_Deleting()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();

                // Act
                var response = await client.GetAsync($"/api/Matches/1001");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            [Fact]
            public async Task Should_Get_NotFound_After_Deleting()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var client = _appFactory.CreateClient();

                // Act
                var deleting = await client.DeleteAsync($"/api/Matches/1001");
                var response = await client.GetAsync($"/api/Matches/1001");

                // Assert
                deleting.Should().NotBeNull();
                deleting.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }


        }
    }
}
