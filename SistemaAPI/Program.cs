using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAPI.Models;
using SistemaAPI.Services;
using Login = SistemaAPI.Models.Login;
using Results = Microsoft.AspNetCore.Http.Results;


var builder = WebApplication.CreateBuilder(args);

// Configurar o contexto do banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adicionar JWT no pipeline de autenticação
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "sistema.com",
            ValidAudience = "sistema.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minhasecretnotavisivelparatodos1234567890"))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<JwtTokenService>();


var app = builder.Build();

// Usar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Configurar o Swagger apenas para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/usuario", [Authorize] async (HttpContext context) =>
{
    return Results.Ok(new { mensagem = "Usuário autenticado" });
});


app.Run();