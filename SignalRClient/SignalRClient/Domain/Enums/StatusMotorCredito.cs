using System.ComponentModel;

namespace SignalRClient.Domain.Enums
{
    public enum StatusMotorCredito : short
    {
        [Description("Cartão Gerado")]
        CartaoGerado = 0,

        [Description("Erro Biometria")]
        ErroBiometria = 1,

        [Description("Cpf Inválido")]
        CpfInvalido = 2,

        [Description("Cpf Cadastrado")]
        CpfCadastrado = 3,

        [Description("Erro Cadastro")]
        ErroCadastro = 4,

        [Description("Erro Solicitacao Credito")]
        ErroSolicitacaoCredito = 5,

        [Description("Erro Gerar Cartão")]
        ErroGerarCartao = 6,

        [Description("Limite Nao Liberado")]
        LimiteNaoLiberado = 7,

        [Description("Já Possui Cartão Havan")]
        JaPossuiCartaoHavan = 8,

        [Description("Limite Aprovado")]
        LimiteAprovado = 9,

        [Description("Erro Não Tratado")]
        ErroNaoTratado = 10,

        [Description("Erro Alteração Cadastro")]
        ErroAlteracaoCadastro = 11,

        [Description("Erro Cliente Não Encontrado")]
        ErroClienteNaoEncontrado = 12,

        [Description("Erro Pessoa Não Encontrada")]
        ErroPessoaNaoEncontrada = 13,

        [Description("Erro Solicitação Não Encontrada")]
        ErroSolicitacaoNaoEncontrada = 14,

        [Description("Erro Falha Ao Obter Solicitação Do Cliente")]
        ErroFalhaAoObterSolicitacaoDoCliente = 15
    }
}
