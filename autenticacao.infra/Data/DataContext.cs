namespace autenticacao.infra.Data
{

    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Identity User
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<RefreshTokenTable> RTokens { get; set; }
    }
}