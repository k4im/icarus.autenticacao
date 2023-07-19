global using autenticacao.domain.Entity;
global using autenticacao.domain.Response;
global using autenticacao.domain.Dtos;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using autenticacao.infra.Data;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;
global using System.Text;
global using Microsoft.IdentityModel.Tokens;
global using autenticacao.infra.Jwt;
global using autenticacao.infra.Key;
global using autenticacao.infra.RefreshToken;
global using Serilog;
global using Serilog.Sinks.Graylog;

