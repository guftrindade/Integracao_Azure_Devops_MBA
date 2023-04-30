using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;

namespace Devops.Services
{
    public class InfrastructureService
    {
        public ResponseResource ServiceRequestResource(RequestResource request)
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
            var requestTime = DateTime.Now;

            return $"Solicitação enviada para o Setor de Infraestrutura em {requestTime} e " +
                   $"dentro de 3 dias úteis uma resposta será enviada para " +
                   $"{email}. Qualquer outra ponderação, favor entrar em contato.";
        }
    }
}
