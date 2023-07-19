namespace autenticacao.infra.Repository
{
    public class RepoAuth : IRepoAuth
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IjwtManager _jwtManager;
        readonly IChaveManager _chaveManager;
        readonly IRefreshManager _refreshManager;

        public RepoAuth(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IjwtManager jwtManager, IChaveManager chaveManager, IRefreshManager refreshManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtManager = jwtManager;
            _chaveManager = chaveManager;
            _refreshManager = refreshManager;
        }

        public RepoAuth(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IjwtManager jwtManager, IChaveManager chaveManager, IRefreshManager refreshManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtManager = jwtManager;
            _chaveManager = chaveManager;
            _refreshManager = refreshManager;
        }

        public async Task<bool> desativarUsuario(string chave)
        {
            var usuario = await _userManager.FindByEmailAsync(chave);
            if (usuario == null) return false;
            usuario.desativarUsuario();
            try
            {
                using (var db = new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Data").Options))
                {
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Response<AppUser>> listarUsuarios(int pagina, float resultado)
        {
            using (var db = new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Data").Options))
            {
                var resultadoPaginas = resultado;
                var pessoas = await db.Users.ToListAsync();
                var totalDePaginas = Math.Ceiling(pessoas.Count() / resultadoPaginas);
                var usersPaginados = pessoas.Skip((pagina - 1) * (int)resultadoPaginas).Take((int)resultadoPaginas).ToList();
                var paginasTotal = (int)totalDePaginas;
                return new Response<AppUser>(usersPaginados, pagina, paginasTotal);
            }
        }

        public async Task<ResponseLoginDTO> logar(LoginDTO loginModel)
        {
            var usuario = await _userManager.FindByNameAsync(loginModel.ChaveDeAcesso);
            if (!usuario.FlagDesativado)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.ChaveDeAcesso, loginModel.Senha, false, true);
                if (result.Succeeded)
                {
                    var token = await _jwtManager.criarAccessToken(loginModel.ChaveDeAcesso);
                    var RToken = _refreshManager.GerarRefreshToken(loginModel.ChaveDeAcesso);
                    await _refreshManager.SalvarRefreshToken(RToken);
                    return new ResponseLoginDTO(token, RToken.Token);
                }
            }
            return new ResponseLoginDTO("Senha ou usuario invalidos!", "Senha ou usuario invalidos!");
        }

        public async Task logOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> reativarUsuario(string chave)
        {
            var usuario = await _userManager.FindByEmailAsync(chave);
            if (usuario == null) return false;
            usuario.reativarUsuario();
            try
            {
                using (var db = new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("Data").Options))
                {
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseRegistroDTO> registrarUsuario(NovoUsuarioDTO user)
        {
            var chave = await _chaveManager.gerarChaveDeAcesso();
            var NovoUsuario = new AppUser
            {
                Name = user.Nome,
                UserName = chave,
                Email = chave,
                EmailConfirmed = true,
                Role = user.Papel.ToUpper()

            };
            var result = await _userManager.CreateAsync(NovoUsuario, user.Senha);
            if (result.Succeeded)
            {
                await criarRoles();
                var appRole = await _roleManager.FindByNameAsync(NovoUsuario.Role);
                await _userManager.SetLockoutEnabledAsync(NovoUsuario, false);
                await _userManager.AddToRoleAsync(NovoUsuario, "ADMIN");
            }
            if (!result.Succeeded && result.Errors.Count() > 0)
            {
                foreach (var erro in result.Errors)
                {
                }
            }
            return new ResponseRegistroDTO(chave);
        }

        async Task criarRoles()
        {
            if (!_roleManager.RoleExistsAsync("ADMIN").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                await _roleManager.CreateAsync(new IdentityRole("ATENDENTE"));
                await _roleManager.CreateAsync(new IdentityRole("FINANCEIRO"));
            }
        }
    }
}