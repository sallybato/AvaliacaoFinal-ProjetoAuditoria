using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;
using Auditoria.Services;

namespace Auditoria.Rotas
{
    public static class DeleteRoutes
    {
        public static void MapDeleteRoutes(this WebApplication app)
        {
            // ========== ROTAS DE LOGS DE ACESSO ==========

            // Exclui um log de acesso específico
            app.MapDelete("/api/logsacesso/{id}", async (int id, HttpRequest request, ControleInternoContext context) =>
            {
                try
                {
                    var log = await context.LogsAcesso.FindAsync(id);

                    if (log == null)
                    {
                        return Results.NotFound($"Log de acesso com ID {id} não encontrado.");
                    }

                    // Obter usuário do header (ou usar o usuário do log se não informado)
                    var usuario = AuditoriaService.ObterUsuarioDoHeader(request);
                    if (usuario == "Sistema")
                    {
                        usuario = log.Usuario; // Usar o usuário do log se não foi informado no header
                    }
                    else if (!await AuditoriaService.ValidarUsuarioAsync(context, usuario))
                    {
                        return Results.BadRequest($"Usuário '{usuario}' não encontrado. Use um usuário válido dos logs de acesso.");
                    }

                    // Salvar dados para trilha de auditoria antes de deletar
                    var dadosAnteriores = new { 
                        id = log.Id, 
                        usuario = log.Usuario, 
                        acao = log.Acao, 
                        ipOrigem = log.IpOrigem, 
                        dataHora = log.DataHora 
                    };

                    context.LogsAcesso.Remove(log);
                    await context.SaveChangesAsync();

                    // Criar trilha de auditoria automaticamente
                    await AuditoriaService.CriarTrilhaDeleteAsync(
                        context,
                        "LogAcesso",
                        usuario,
                        dadosAnteriores
                    );

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE PERMISSÕES ==========

            // Exclui uma permissão específica
            app.MapDelete("/api/permissoes/{id}", async (int id, HttpRequest request, ControleInternoContext context) =>
            {
                try
                {
                    var permissao = await context.Permissoes.FindAsync(id);

                    if (permissao == null)
                    {
                        return Results.NotFound($"Permissão com ID {id} não encontrada.");
                    }

                    // Obter usuário do header
                    var usuario = AuditoriaService.ObterUsuarioDoHeader(request);
                    
                    // Validar usuário se não for "Sistema"
                    if (usuario != "Sistema" && !await AuditoriaService.ValidarUsuarioAsync(context, usuario))
                    {
                        return Results.BadRequest($"Usuário '{usuario}' não encontrado. Use um usuário válido dos logs de acesso.");
                    }

                    // Salvar dados para trilha de auditoria antes de deletar
                    var dadosAnteriores = new { 
                        id = permissao.Id, 
                        nome = permissao.Nome, 
                        nivel = permissao.Nivel, 
                        descricao = permissao.Descricao 
                    };

                    context.Permissoes.Remove(permissao);
                    await context.SaveChangesAsync();

                    // Criar trilha de auditoria automaticamente
                    await AuditoriaService.CriarTrilhaDeleteAsync(
                        context,
                        "Permissao",
                        usuario,
                        dadosAnteriores
                    );

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE POLÍTICAS ==========

            // Exclui uma política específica
            app.MapDelete("/api/politicas/{id}", async (int id, HttpRequest request, ControleInternoContext context) =>
            {
                try
                {
                    var politica = await context.Politicas.FindAsync(id);

                    if (politica == null)
                    {
                        return Results.NotFound($"Política com ID {id} não encontrada.");
                    }

                    // Obter usuário do header
                    var usuario = AuditoriaService.ObterUsuarioDoHeader(request);
                    
                    // Validar usuário se não for "Sistema"
                    if (usuario != "Sistema" && !await AuditoriaService.ValidarUsuarioAsync(context, usuario))
                    {
                        return Results.BadRequest($"Usuário '{usuario}' não encontrado. Use um usuário válido dos logs de acesso.");
                    }

                    // Salvar dados para trilha de auditoria antes de deletar
                    var dadosAnteriores = new { 
                        id = politica.Id, 
                        nome = politica.Nome, 
                        descricao = politica.Descricao, 
                        ativa = politica.Ativa, 
                        dataCriacao = politica.DataCriacao 
                    };

                    context.Politicas.Remove(politica);
                    await context.SaveChangesAsync();

                    // Criar trilha de auditoria automaticamente
                    await AuditoriaService.CriarTrilhaDeleteAsync(
                        context,
                        "Politica",
                        usuario,
                        dadosAnteriores
                    );

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE TRILHAS DE AUDITORIA ==========

            // Exclui uma trilha de auditoria específica
            app.MapDelete("/api/trilhasauditoria/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var trilha = await context.TrilhasAuditoria.FindAsync(id);

                    if (trilha == null)
                    {
                        return Results.NotFound($"Trilha de auditoria com ID {id} não encontrada.");
                    }

                    context.TrilhasAuditoria.Remove(trilha);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
        }
    }
}

