using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;

namespace Auditoria.Services
{
    /// <summary>
    /// Serviço para criação automática de trilhas de auditoria
    /// </summary>
    public static class AuditoriaService
    {
        /// <summary>
        /// Cria uma trilha de auditoria automaticamente
        /// </summary>
        public static async Task CriarTrilhaAuditoriaAsync(
            ControleInternoContext context,
            string entidadeAfetada,
            string tipoOperacao,
            string usuario,
            object? dadosAnteriores = null,
            object? dadosNovos = null)
        {
            try
            {
                var trilha = new TrilhaAuditoria
                {
                    EntidadeAfetada = entidadeAfetada,
                    TipoOperacao = tipoOperacao,
                    Usuario = usuario,
                    DataHora = DateTime.Now,
                    DadosAnteriores = dadosAnteriores != null ? JsonSerializer.Serialize(dadosAnteriores) : null,
                    DadosNovos = dadosNovos != null ? JsonSerializer.Serialize(dadosNovos) : null
                };

                context.TrilhasAuditoria.Add(trilha);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Log do erro, mas não interrompe a operação principal
                // Em produção, você pode querer logar isso em um sistema de logs
            }
        }

        /// <summary>
        /// Cria uma trilha de auditoria para criação de entidade
        /// </summary>
        public static async Task CriarTrilhaCreateAsync(
            ControleInternoContext context,
            string entidadeAfetada,
            string usuario,
            object dadosNovos)
        {
            await CriarTrilhaAuditoriaAsync(context, entidadeAfetada, "CREATE", usuario, null, dadosNovos);
        }

        /// <summary>
        /// Cria uma trilha de auditoria para atualização de entidade
        /// </summary>
        public static async Task CriarTrilhaUpdateAsync(
            ControleInternoContext context,
            string entidadeAfetada,
            string usuario,
            object dadosAnteriores,
            object dadosNovos)
        {
            await CriarTrilhaAuditoriaAsync(context, entidadeAfetada, "UPDATE", usuario, dadosAnteriores, dadosNovos);
        }

        /// <summary>
        /// Cria uma trilha de auditoria para exclusão de entidade
        /// </summary>
        public static async Task CriarTrilhaDeleteAsync(
            ControleInternoContext context,
            string entidadeAfetada,
            string usuario,
            object dadosAnteriores)
        {
            await CriarTrilhaAuditoriaAsync(context, entidadeAfetada, "DELETE", usuario, dadosAnteriores, null);
        }

        /// <summary>
        /// Valida se o usuário existe nos logs de acesso
        /// </summary>
        public static async Task<bool> ValidarUsuarioAsync(ControleInternoContext context, string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            return await context.LogsAcesso
                .AnyAsync(l => l.Usuario == usuario);
        }

        /// <summary>
        /// Obtém o usuário do header HTTP ou retorna "Sistema" como padrão
        /// </summary>
        public static string ObterUsuarioDoHeader(HttpRequest request)
        {
            if (request.Headers.TryGetValue("X-Usuario", out var usuarioHeader))
            {
                var usuario = usuarioHeader.ToString().Trim();
                return string.IsNullOrWhiteSpace(usuario) ? "Sistema" : usuario;
            }
            return "Sistema";
        }
    }
}

