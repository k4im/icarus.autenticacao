namespace autenticacao.domain.Dtos
{
    public class NovoUsuarioDTO
    {
        public NovoUsuarioDTO(string senha, string papel, string nome)
        {
            Senha = senha;
            Papel = papel;
            Nome = nome;
        }
        public string Nome { get; }
        public string Senha { get; }
        public string Papel { get; }
    }
}