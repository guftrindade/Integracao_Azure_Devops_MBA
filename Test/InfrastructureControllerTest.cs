using Devops.Controllers;
using Microsoft.AspNetCore.Mvc;
using Devops.Services.Interfaces;
using Moq;
using Test.Mocks;

namespace Test
{
    public class InfrastructureControllerTest
    {
        private readonly Mock<IInfrastructureService> _infrastructureServiceMock;

        public InfrastructureControllerTest()
        {
            _infrastructureServiceMock = new Mock<IInfrastructureService>();
        }

        private InfrastructureController GetController()
        {
            return new InfrastructureController(_infrastructureServiceMock.Object);
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