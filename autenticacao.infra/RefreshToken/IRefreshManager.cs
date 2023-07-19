namespace autenticacao.infra.RefreshToken
{
    public interface IRefreshManager
    {
        Task<autenticacao.domain.Tokens.RefreshToken> BuscarRefreshToken(string chave, string refreshToken);
        autenticacao.domain.Tokens.RefreshToken GerarRefreshToken(string ChaveDeAcesso);

        Task<bool> SalvarRefreshToken(autenticacao.domain.Tokens.RefreshToken request);

        Task<bool> DeletarRefreshToken(string username);
    }
}