using SignalRClient.Domain.Enums;

namespace SignalRClient.Domain.DTOs.OutputModels
{
    public class RetornoMotorCreditoOutputModel
    {
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
        public StatusMotorCredito Status { get; set; }
    }
}
