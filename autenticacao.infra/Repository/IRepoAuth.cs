namespace autenticacao.infra.Repository
{
    public interface IRepoAuth
    {
        Task<Response<UserDTO>> listarUsuarios(int pagina, float resultado);
        Task<ResponseRegistroDTO> registrarUsuario(NovoUsuarioDTO user);
        Task<Object> RefreshAccessToken(string chave, string TokenDeRefresh);
        Task<bool> desativarUsuario(string chave);
        Task<bool> reativarUsuario(string chave);
        Task<Object> logar(LoginDTO loginModel);
        Task logOut();

    }
}