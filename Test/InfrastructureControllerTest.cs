using Devops.Controllers;
using Devops.ViewModels.Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using Test.Mocks;

namespace Test
{
    public class InfrastructureControllerTest
    {
        private static InfrastructureController GetController()
        {
            return new InfrastructureController();
        }

        [Fact]
        public void RequestResource_ShouldReturnStatusCode200()
        {
            var requestMock = ResourceMock.RequestResourceFaker.Generate();

            var response = GetController().RequestResource(requestMock);

            var result = Assert.IsType<OkObjectResult>(response.Result);
            Assert.NotNull(result.Value);
        }
    }
}