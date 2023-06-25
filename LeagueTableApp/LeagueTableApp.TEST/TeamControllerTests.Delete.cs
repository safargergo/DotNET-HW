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
        public class Delete : TeamControllerTests
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
                var response = await client.GetAsync($"/api/Teams/104");
                //var team = await client.GetFromJsonAsync<Team>($"/api/Teams/104", _serializerOptions);

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
                var deleting = await client.DeleteAsync($"/api/Teams/104");
                var response = await client.GetAsync($"/api/Teams/104");
                //var team = await client.GetFromJsonAsync<Team>($"/api/Teams/104", _serializerOptions);

                // Assert
                deleting.Should().NotBeNull();
                deleting.StatusCode.Should().Be(HttpStatusCode.NoContent);
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            

        }
    }
}
