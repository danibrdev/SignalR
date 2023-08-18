using SignalRServer.Domain.Enums;

namespace SignalRServer.Domain.DTOs.RequestModel
{
    public class RetornoMotorCreditoRequestModel
    {
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
        public StatusMotorCredito Status { get; set; }

        public RetornoMotorCreditoRequestModel() { }
        public RetornoMotorCreditoRequestModel(string usuario, string mensagem, StatusMotorCredito status)
        {
            Usuario = usuario;
            Mensagem = mensagem;
            Status = status;
        }
    }
}
