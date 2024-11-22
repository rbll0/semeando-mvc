using Microsoft.EntityFrameworkCore;
using semeando_mvc.Application.Services;
using semeando_mvc.Infrastructure.Data.Context;
using semeando_mvc.Infrastructure.Interfaces;
using semeando_mvc.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext e Injeção de Dependência
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// Registrar repositórios e serviços no contêiner de DI
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Adicionar suporte à sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define o tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Torna o cookie acessível apenas pelo HTTP
    options.Cookie.IsEssential = true; // Garante que o cookie seja armazenado mesmo sem consentimento
});

// Adicionar serviços MVC
builder.Services.AddControllersWithViews();

// Configurar o Swagger para documentação da API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Semeando API",
        Version = "v1",
        Description = "Documentação da API para o projeto Semeando",
        Contact = new OpenApiContact
        {
            Name = "Semeando",
            Email = "plantecomsemeando@gmail.com",
            Url = new Uri("https://github.com/seu-usuario") // Atualize com o URL correto
        }
    });
});

// Configurar Kestrel para rodar na porta 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Configura para escutar na porta 5000
});

var app = builder.Build();

// Configuração do pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adicionar suporte à sessão ao pipeline
app.UseSession();

// Adicionar suporte ao Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Semeando API v1");
    c.RoutePrefix = "swagger"; // Acesse a documentação em /swagger
});

// Configuração de autorização
app.UseAuthorization();

// Configurar rotas padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Iniciar a aplicação
app.Run();
