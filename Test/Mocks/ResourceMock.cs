using Bogus;
using Devops.ViewModels.Infrastructure.Enums;
using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;

namespace Test.Mocks
{
    public static class ResourceMock
    {
        public static Faker<RequestResource> RequestResourceFaker =>
            new Faker<RequestResource>()
            .CustomInstantiator(faker => new RequestResource()
            {
                ResourceName = "ResourceName_Test",
                ResourceAzureType = ResourceAzureType.WEB_APP,
                Note = "TDD note",
                Email = "emailtest@test.com"
            });

        public static Faker<ResponseResource> ResponseResourceFaker =>
            new Faker<ResponseResource>()
            .CustomInstantiator(faker => new ResponseResource()
            {
                Status = "Em aprovação",
                Note = "TDD note",
                ProtocolNumber = 45678
            });
    }
}
