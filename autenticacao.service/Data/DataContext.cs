namespace autenticacao.service.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Identity User
            base.OnModelCreating(modelBuilder);
            
            #region Nome 

            modelBuilder.Entity<Pessoa>(builder =>{
                    builder.OwnsOne<Nome>(pessoa => pessoa.Nome)
                    .Property(nome => nome.PrimeiroNome)
                    .HasColumnName("PrimeiroNome")
                    .IsRequired(true);


                    builder.OwnsOne<Nome>(pessoa => pessoa.Nome)
                    .Property(nome => nome.SobreNome)
                    .HasColumnName("SobreNome")
                    .IsRequired(true);

                });
            #endregion
            
            #region  Endereco
                modelBuilder.Entity<Pessoa>(builder => {
                    builder.OwnsOne<Endereco>(pessoa => pessoa.Endereco)
                    .Property(endereco => endereco.Cidade)
                    .HasColumnName("Cidade")
                    .IsRequired(false);

                    builder.OwnsOne<Endereco>(pessoa => pessoa.Endereco)
                    .Property(endereco => endereco.Bairro)
                    .HasColumnName("Bairro")
                    .IsRequired(false);
                    
                    builder.OwnsOne<Endereco>(pessoa => pessoa.Endereco)
                    .Property(endereco => endereco.Cep)
                    .HasColumnName("Cep")
                    .IsRequired(false);
                    builder.OwnsOne<Endereco>(pessoa => pessoa.Endereco)
                    .Property(endereco => endereco.Numero)
                    .HasColumnName("NumeroEndereco")
                    .IsRequired(true);
                });

            #endregion
        
            #region Telefone
                modelBuilder.Entity<Pessoa>(builder => {
                    builder.OwnsOne<Telefone>(pessoa => pessoa.Telefone)
                    .Property(telefone => telefone.CodigoPais)
                    .HasColumnName("Pais")
                    .IsRequired(false);

                    builder.OwnsOne<Telefone>(pessoa => pessoa.Telefone)
                    .Property(telefone => telefone.CodigoDeArea)
                    .HasColumnName("Area")
                    .IsRequired(false);

                    builder.OwnsOne<Telefone>(pessoa => pessoa.Telefone)
                    .Property(telefone => telefone.Numero)
                    .HasColumnName("NumeroTelefone")
                    .IsRequired(true);
                });
            #endregion
            
            #region  CPF
                modelBuilder.Entity<Pessoa>(builder => {
                    builder.OwnsOne<CadastroPessoaFisica>(pessoa => pessoa.Cpf)
                    .Property(cpf => cpf.Cpf)
                    .HasColumnName("cpf")
                    .IsRequired(true);
                });
            #endregion
        }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<RefreshTokenTable> RTokens { get; set; }
    }
}