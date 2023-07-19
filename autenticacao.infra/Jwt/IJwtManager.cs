namespace autenticacao.infra.Jwt
{
    public interface IjwtManager
    {
        Task<string> criarAccessToken(string chaveDeAcesso);
        List<Claim> gerarClaims(AppUser user);
    }
}