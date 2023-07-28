global using System;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Claims;
global using System.Text;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using System.Reflection;
global using Swashbuckle.AspNetCore.Filters;
global using autenticacao.service.Extensions;

/*Usings atribuidos aos dominios*/
global using autenticacao.domain.Dtos;
global using autenticacao.domain.Entity;

/*Usings atribuidos a parte de infra*/
global using autenticacao.infra.Data;
global using autenticacao.infra.Repository;
global using autenticacao.infra.Jwt;
global using autenticacao.infra.Key;
global using autenticacao.infra.RefreshToken;
global using autenticacao.infra.Log;
