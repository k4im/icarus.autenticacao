namespace autenticacao.infra.Repository
{
    public interface IRepoAuth
    {
        Task<Response<UserDTO>> listarUsuarios(int pagina, float resultado);
        Task<ResponseRegistroDTO> registrarUsuario(NovoUsuarioDTO user);
        Task<bool> desativarUsuario(string chave);
        Task<bool> reativarUsuario(string chave);
        Task<ResponseLoginDTO> logar(LoginDTO loginModel);
        Task logOut();

    }
}