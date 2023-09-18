namespace autenticacao.service.Controllers
{
    [ApiController]
    [Route("api/usuarios")]

    public class UsuarioController : ControllerBase
    {
        readonly IRepoAuth _repoAuth;
        readonly IRefreshManager _refreshManager;


        public UsuarioController(IRepoAuth repoAuth, IRefreshManager refreshManager)
        {
            _repoAuth = repoAuth;
            _refreshManager = refreshManager;
        }

        /// <summary>
        /// Retorna a lista de usuarios de forma paginada.
        /// </summary>
        /// <remarks>
        /// **Caso não seja adicionado um valor de pagina ou resultado, será atribuido como padrão o valor para pagina de 1 e o valor para o resultado por pagina de 5.**
        /// </remarks>
        /// <returns code="200">Retorna codigo 200 com a lista de usuarios</returns>
        /// <returns code="404">Informa que não existe uma lista de usuario</returns>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpGet("{pagina?}/{resultado?}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> buscarUsuarios(int pagina = 1, float resultado = 5)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var usuarios = await _repoAuth.listarUsuarios(pagina, resultado);
            if (usuarios == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, usuarios);
        }

        /// <summary>
        /// Cria novo usuario.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///     
        ///     {
        ///       "nome": "Jonas"            
        ///       "senha": "Sua Senha",
        ///       "papel": "ADMIN ou ATENDENTE"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a pessoa com o dado atualizado</response>
        /// <response code="500">Retorna que algo deu errado</response>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpPost("novo")]
        [AllowAnonymous]
        public async Task<IActionResult> criarUsuario(NovoUsuarioDTO user)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _repoAuth.registrarUsuario(user);
                if (result == null)
                {
                    return StatusCode(500, "Algo deu errado!");
                }
                return StatusCode(200, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo deu errado!");
            }
        }

        /// <summary>
        /// Ira realizar a operação de desativar um usuario
        /// </summary>
        /// <remarks>
        /// Caso o usuario seja desativado o mesmo não conseguira realizar mais login. É possivel estar reativando o usuario, porém enquanto o status permanecer o mesmo não tera mais acesso.
        /// </remarks>
        /// <returns code="200">Informa que foi possivel estar desativando o usuario</returns>
        /// <returns code="500">Informa que não foi possivel realizar a operação</returns>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpPost("desativar/{chave}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> desativarUsuario([FromRoute] string chave)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await _repoAuth.desativarUsuario(chave);
            if (result)
            {
                return StatusCode(200, "Usuario desativado com sucesso!");
            }
            return StatusCode(500, "Não foi possivel desativar o usuario");
        }

        /// <summary>
        /// Realizar login.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///     
        ///     {
        ///       "chaveDeAcesso": "Sua chave de acesso",
        ///       "senha": "Sua senha"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna a pessoa com o dado atualizado</response>
        /// <response code="500">Retorna que algo deu errado</response>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginDTO login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _repoAuth.logar(login);
                if (result == null)
                {
                    return NotFound();
                }
                return StatusCode(200, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo deu errado!");
            }
        }

        /// <summary>
        /// Ira realizar o LogOut do usuario que está atualmente conectado
        /// </summary>
        /// <returns code="200">Informa que o usuario conseguiu se desconectar</returns>
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> logOut()
        {
            await _repoAuth.logOut();
            return StatusCode(200, "Você realizou logout");
        }

        /// <summary>
        /// Ira realizar a operação de reativar um usuario
        /// </summary>
        /// <returns code="200">Informa que foi possivel estar desativando o usuario</returns>
        /// <returns code="500">Informa que não foi possivel realizar a operação</returns>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpPost("reativar/{chave}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> reavitarUsuario([FromRoute] string chave)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await _repoAuth.reativarUsuario(chave);
            if (result)
            {
                return StatusCode(200, "Usuario reativado com sucesso!");
            }
            return StatusCode(500, "Não foi possivel desativar o usuario");
        }

        /// <summary>
        /// Ira realizar a operação de atualizar o token de acesso
        /// </summary>
        /// <returns code="200">Retorna o novo token de acesso</returns>
        /// <returns code="500">Informa que não foi possivel realizar a operação</returns>
        /// <returns code="401">Informa que não está autorizado para a funcao</returns>
        /// <returns code="403">Informa que não tem privilégios para a funcao</returns>
        [HttpPost("reativar/{chave}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RefreshToken([FromRoute] string chave)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await _repoAuth.reativarUsuario(chave);
            if (result)
            {
                return StatusCode(200, "Usuario reativado com sucesso!");
            }
            return StatusCode(500, "Não foi possivel desativar o usuario");
        }
    }
}