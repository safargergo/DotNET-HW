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
        public class Delete : LeagueControllerTests
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
                var response = await client.GetAsync($"/api/Leagues/102");

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
                var deleting = await client.DeleteAsync($"/api/Leagues/102");
                var response = await client.GetAsync($"/api/Leagues/102");

                // Assert
                deleting.Should().NotBeNull();
                deleting.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }


        }
    }
}
