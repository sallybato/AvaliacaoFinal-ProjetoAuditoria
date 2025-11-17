using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Rotas;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework com SQLite
builder.Services.AddDbContext<ControleInternoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Data Source=ControleInterno.db"));

var app = builder.Build();

// Habilitar arquivos estáticos
app.UseStaticFiles();

// Rota para servir a página inicial
app.MapGet("/", () => Results.Redirect("/index.html"));

// Registrar todas as rotas organizadas por tipo de operação
app.MapGetRoutes();
app.MapPostRoutes();
app.MapPutRoutes();
app.MapDeleteRoutes();

// Garantir que o banco de dados seja criado e populado
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ControleInternoContext>();
    context.Database.EnsureCreated();
    
    // Popular o banco com dados iniciais se estiver vazio
    if (!context.Politicas.Any())
    {
        SeedData.Initialize(context);
    }
}

app.Run();
