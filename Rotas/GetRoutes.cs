using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;

namespace Auditoria.Rotas
{
    public static class GetRoutes
    {
        public static void MapGetRoutes(this WebApplication app)
        {
            // ========== ROTAS DE USUÁRIOS ==========
            
            // Lista todos os usuários únicos dos logs de acesso
            app.MapGet("/api/usuarios", async (ControleInternoContext context) =>
            {
                try
                {
                    var usuarios = await context.LogsAcesso
                        .Select(l => l.Usuario)
                        .Distinct()
                        .OrderBy(u => u)
                        .ToListAsync();
                    return Results.Ok(usuarios);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE LOGS DE ACESSO ==========
            
            // Lista todos os logs de acesso
            app.MapGet("/api/logsacesso", async (ControleInternoContext context) =>
            {
                try
                {
                    var logs = await context.LogsAcesso
                        .OrderByDescending(l => l.DataHora)
                        .ToListAsync();
                    return Results.Ok(logs);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // Busca um log de acesso específico pelo ID
            app.MapGet("/api/logsacesso/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var log = await context.LogsAcesso.FindAsync(id);

                    if (log == null)
                    {
                        return Results.NotFound($"Log de acesso com ID {id} não encontrado.");
                    }

                    return Results.Ok(log);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE PERMISSÕES ==========

            // Lista todas as permissões
            app.MapGet("/api/permissoes", async (ControleInternoContext context) =>
            {
                try
                {
                    var permissoes = await context.Permissoes.ToListAsync();
                    return Results.Ok(permissoes);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // Busca uma permissão específica pelo ID
            app.MapGet("/api/permissoes/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var permissao = await context.Permissoes.FindAsync(id);

                    if (permissao == null)
                    {
                        return Results.NotFound($"Permissão com ID {id} não encontrada.");
                    }

                    return Results.Ok(permissao);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE POLÍTICAS ==========

            // Lista todas as políticas
            app.MapGet("/api/politicas", async (ControleInternoContext context) =>
            {
                try
                {
                    var politicas = await context.Politicas.ToListAsync();
                    return Results.Ok(politicas);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // Busca uma política específica pelo ID
            app.MapGet("/api/politicas/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var politica = await context.Politicas.FindAsync(id);

                    if (politica == null)
                    {
                        return Results.NotFound($"Política com ID {id} não encontrada.");
                    }

                    return Results.Ok(politica);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE TRILHAS DE AUDITORIA ==========

            // Lista todas as trilhas de auditoria
            app.MapGet("/api/trilhasauditoria", async (ControleInternoContext context) =>
            {
                try
                {
                    var trilhas = await context.TrilhasAuditoria
                        .OrderByDescending(t => t.DataHora)
                        .ToListAsync();
                    return Results.Ok(trilhas);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
            
            // Estatísticas do sistema
            app.MapGet("/api/estatisticas", async (ControleInternoContext context) =>
            {
                try
                {
                    var totalPoliticas = await context.Politicas.CountAsync();
                    var politicasAtivas = await context.Politicas.CountAsync(p => p.Ativa);
                    var politicasInativas = totalPoliticas - politicasAtivas;
                    
                    var totalPermissoes = await context.Permissoes.CountAsync();
                    
                    var totalLogs = await context.LogsAcesso.CountAsync();
                    var logsHoje = await context.LogsAcesso
                        .CountAsync(l => l.DataHora.Date == DateTime.Today);
                    
                    var totalTrilhas = await context.TrilhasAuditoria.CountAsync();

                    // Agrupar logs por ação
                    var logsPorAcao = await context.LogsAcesso
                        .GroupBy(l => l.Acao)
                        .Select(g => new { Acao = g.Key, Quantidade = g.Count() })
                        .OrderByDescending(x => x.Quantidade)
                        .ToListAsync();

                    return Results.Ok(new
                    {
                        politicas = new
                        {
                            total = totalPoliticas,
                            ativas = politicasAtivas,
                            inativas = politicasInativas
                        },
                        permissoes = new { total = totalPermissoes },
                        logs = new
                        {
                            total = totalLogs,
                            hoje = logsHoje
                        },
                        trilhas = new { total = totalTrilhas },
                        logsPorAcao = logsPorAcao
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // Busca uma trilha de auditoria específica pelo ID
            app.MapGet("/api/trilhasauditoria/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var trilha = await context.TrilhasAuditoria.FindAsync(id);

                    if (trilha == null)
                    {
                        return Results.NotFound($"Trilha de auditoria com ID {id} não encontrada.");
                    }

                    return Results.Ok(trilha);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
        }
    }
}

