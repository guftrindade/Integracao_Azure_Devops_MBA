using Infraestrutura.Api.Models.Request;
using Infraestrutura.Api.Models.Response;
using Infraestrutura.Api.Services.Interfaces;

namespace Infraestrutura.Api.Services
{
    public class InfrastructureService : IInfrastructureService
    {
        public async Task<ResponseResource> RequestResource(RequestResource request)
        {

            return new ResponseResource
            {
                Status = "Em processo de aprovação",
                ProtocolNumber = 15468,
                Note = NoteMessage(request.Email)
            };
        }

        private static string NoteMessage(string email)
        {
            var requestTime = DateTime.UtcNow;

            return $"Solicitação enviada para o Setor de Infraestrutura em {requestTime} e " +
                   $"dentro de 3 dias úteis uma resposta será enviada para " +
                   $"{email}. Qualquer outra ponderação, favor entrar em contato.";
        }
    }
}
