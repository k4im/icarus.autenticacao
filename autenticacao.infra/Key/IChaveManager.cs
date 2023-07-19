namespace autenticacao.infra.Key
{
    public interface IChaveManager
    {
        Task<string> gerarChaveDeAcesso();
    }
}