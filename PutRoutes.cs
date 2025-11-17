using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;
using Auditoria.Services;

namespace Auditoria.Rotas
{
    public static class PutRoutes
    {
        public static void MapPutRoutes(this WebApplication app)
        {
            // ========== ROTAS DE POLÍTICAS ==========

            // Atualiza uma política existente
            app.MapPut("/api/politicas/{id}", async (int id, Politica politicaAtualizada, HttpRequest request, ControleInternoContext context) =>
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

                    // Salvar dados anteriores para trilha de auditoria
                    var dadosAnteriores = new { 
                        id = politica.Id, 
                        nome = politica.Nome, 
                        descricao = politica.Descricao, 
                        ativa = politica.Ativa 
                    };

                    // Atualizar apenas os campos permitidos
                    politica.Nome = politicaAtualizada.Nome;
                    politica.Descricao = politicaAtualizada.Descricao;
                    politica.Ativa = politicaAtualizada.Ativa;
                    // DataCriacao não deve ser alterada

                    await context.SaveChangesAsync();

                    // Criar trilha de auditoria automaticamente
                    var dadosNovos = new { 
                        id = politica.Id, 
                        nome = politica.Nome, 
                        descricao = politica.Descricao, 
                        ativa = politica.Ativa 
                    };
                    await AuditoriaService.CriarTrilhaUpdateAsync(
                        context,
                        "Politica",
                        usuario,
                        dadosAnteriores,
                        dadosNovos
                    );

                    return Results.Ok(politica);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // Alterna o status ativo/inativo de uma política
            app.MapPut("/api/politicas/{id}/toggle-status", async (int id, HttpRequest request, ControleInternoContext context) =>
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

                    // Salvar dados anteriores para trilha de auditoria
                    var dadosAnteriores = new { 
                        id = politica.Id, 
                        nome = politica.Nome, 
                        descricao = politica.Descricao, 
                        ativa = politica.Ativa 
                    };

                    // Alternar status
                    politica.Ativa = !politica.Ativa;

                    await context.SaveChangesAsync();

                    // Criar trilha de auditoria automaticamente
                    var dadosNovos = new { 
                        id = politica.Id, 
                        nome = politica.Nome, 
                        descricao = politica.Descricao, 
                        ativa = politica.Ativa 
                    };
                    await AuditoriaService.CriarTrilhaUpdateAsync(
                        context,
                        "Politica",
                        usuario,
                        dadosAnteriores,
                        dadosNovos
                    );

                    return Results.Ok(new { 
                        id = politica.Id, 
                        ativa = politica.Ativa,
                        message = politica.Ativa ? "Política ativada com sucesso." : "Política desativada com sucesso."
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
        }
    }
}

