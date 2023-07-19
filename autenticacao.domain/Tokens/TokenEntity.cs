namespace autenticacao.domain.Tokens
{
    public class TokenEntity
    {
        public TokenEntity(string token)
        {
            Token = token;
            DataDeCriacao = DateTime.UtcNow;
            DataDeExpiracao = DateTime.UtcNow.AddHours(1);
        }

        public TokenEntity(string token, DateTime dataDeCriacao, DateTime dataDeExpiracao)
        {
            Token = token;
            DataDeCriacao = dataDeCriacao;
            DataDeExpiracao = dataDeExpiracao;
        }

        [DataType("NVARCHAR")]
        public string Token { get; }

        [DataType("DATETIME")]
        public DateTime DataDeCriacao { get; }

        [DataType("DATETIME")]
        public DateTime DataDeExpiracao { get; }

        public bool tokenExpirado()
        {
            if (this.DataDeExpiracao < DateTime.UtcNow) return true;
            return false;
        }
    }
}