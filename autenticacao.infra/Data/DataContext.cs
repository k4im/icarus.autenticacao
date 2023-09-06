namespace autenticacao.infra.Data
{

    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions options) : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured) 
            {
                var conn = Environment.GetEnvironmentVariable("DB_CONNECTION");
                var versionMysql = new MySqlServerVersion(new Version(8,10,31));
                optionsBuilder.UseMySql(conn, versionMysql);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Identity User
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<RefreshTokenTable> RTokens { get; set; }
    }
}