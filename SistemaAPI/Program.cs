using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.EntityFrameworkCore;
using SistemaAPI.Models;

var builder = WebApplication.CreateBuilder(args);


// Adicionar configurações de banco de dados
var connectionString = "Data Source=Users.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

// Configurar o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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


var app = builder.Build();

// Usar o CORS
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


// Configurar o Swagger apenas para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();
