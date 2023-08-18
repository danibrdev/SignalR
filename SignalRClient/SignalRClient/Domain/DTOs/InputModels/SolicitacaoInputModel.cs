using SignalRClient.Domain.Enums;

namespace SignalRClient.Domain.DTOs.InputModels
{
    public class SolicitacaoInputModel
    {
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
        public StatusMotorCredito Status { get; set; }
    }
}
