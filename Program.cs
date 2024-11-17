using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddScoped<ExoContext, ExoContext>();

// Adicionando os controladores ao pipeline
builder.Services.AddControllers();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração da autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem está solicitando.
        ValidateIssuer = true,
        
        // Valida quem está recebendo.
        ValidateAudience = true,

        // Define se o tempo de expiração será validado.
        ValidateLifetime = true,  // Corrigido o nome da propriedade

        // Criptografia e validação da chave de autenticação.
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),

        // Valida o tempo de expiração do token.
        ClockSkew = TimeSpan.FromMinutes(30),

        // Nome do issuer (emissor) da origem.
        ValidIssuer = "exoapi.webapi",

        // Nome do audience (destinatário) do token.
        ValidAudience = "exoapi.webapi"
    };
});

// Adicionando os repositórios
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment()) // Corrigido para verificar se o ambiente é de desenvolvimento
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
