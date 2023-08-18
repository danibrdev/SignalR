using SignalRServer.Domain.Enums;

namespace SignalRServer.Domain.DTOs.ResponseModel
{
    public class RetornoSolicitacaoResponseModel
    {
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
        public StatusMotorCredito Status { get; set; }
    }
}
