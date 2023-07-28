namespace autenticacao.service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPasswordConfiguration(this IServiceCollection service)
        {
            service.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(50);
                opt.Lockout.MaxFailedAccessAttempts = 2;
            });
            return service;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection service)
        {
            service.AddIdentity<AppUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();
            return service;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection service)
        {
            service.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Pessoas & usuarios API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Adicione o token para logar",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
            service.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            return service;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepoAuth, RepoAuth>();
            services.AddScoped<IRefreshManager, RefreshManager>();
            services.AddScoped<IChaveManager, ChaveManager>();
            services.AddScoped<IjwtManager, jwtManager>();
            services.AddScoped<Logger>();
            services.AddAutoMapper(typeof(Program).Assembly);
            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,


                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]))
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}