
using Microsoft.AspNetCore.Http;

namespace autenticacao.infra.RefreshToken
{

    public class RefreshManager : IRefreshManager
    {
        readonly DataContext _db;

        public RefreshManager(DataContext db)
            => _db = db;
        public async Task<autenticacao.domain.Tokens.RefreshToken> BuscarRefreshToken(string chave, string refreshToken)
        {

            var item = await _db.RTokens.FirstOrDefaultAsync(x => x.Usuario == chave);
            if (item == null) Results.NotFound("Não existe um refresh token para esse usuario");
            var RToken = new autenticacao.domain.Tokens.RefreshToken(item.RToken, item.Usuario, item.DataCriacao, item.DataExpiracao);
            return RToken;

        }
        public async Task<bool> DeletarRefreshToken(string token)
        {

            var refreshToken = await _db.RTokens.FirstOrDefaultAsync(x => x.RToken == token);
            if (refreshToken == null) Results.NotFound();
            try
            {
                _db.RTokens.Remove(refreshToken);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public autenticacao.domain.Tokens.RefreshToken GerarRefreshToken(string ChaveDeAcesso)
        {
            var random = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 50).Select(x => pool[random.Next(0, pool.Length)]);
            var randomToken = new string(chars.ToArray());
            byte[] tokenBytes = Encoding.UTF8.GetBytes(randomToken.ToString());
            var tokenConverted = Convert.ToBase64String(tokenBytes);

            return new autenticacao.domain.Tokens.RefreshToken(tokenConverted, ChaveDeAcesso);
        }

        public async Task<bool> SalvarRefreshToken(autenticacao.domain.Tokens.RefreshToken request)
        {
            using (var db = new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Data").Options))
            {
                var tokenConvertido = new RefreshTokenTable
                {
                    Usuario = request.Usuario,
                    DataCriacao = request.DataDeCriacao,
                    DataExpiracao = request.DataDeExpiracao,
                    RToken = request.Token
                };
                try
                {
                    db.RTokens.Add(tokenConvertido);
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}