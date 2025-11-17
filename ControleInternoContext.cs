using Microsoft.EntityFrameworkCore;
using Auditoria.Models;

namespace Auditoria.Data
{
    /// <summary>
    /// Contexto do Entity Framework para o sistema de Controle Interno
    /// </summary>
    public class ControleInternoContext : DbContext
    {
        public ControleInternoContext(DbContextOptions<ControleInternoContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet para políticas de controle interno
        /// </summary>
        public DbSet<Politica> Politicas { get; set; }

        /// <summary>
        /// DbSet para permissões de acesso
        /// </summary>
        public DbSet<Permissao> Permissoes { get; set; }

        /// <summary>
        /// DbSet para logs de acesso
        /// </summary>
        public DbSet<LogAcesso> LogsAcesso { get; set; }

        /// <summary>
        /// DbSet para trilhas de auditoria
        /// </summary>
        public DbSet<TrilhaAuditoria> TrilhasAuditoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações específicas para cada entidade
            modelBuilder.Entity<Politica>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("datetime('now')");
            });

            modelBuilder.Entity<Permissao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Nivel).IsRequired();
            });

            modelBuilder.Entity<LogAcesso>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Usuario).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Acao).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IpOrigem).IsRequired().HasMaxLength(45);
                entity.Property(e => e.DataHora).HasDefaultValueSql("datetime('now')");
            });

            modelBuilder.Entity<TrilhaAuditoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EntidadeAfetada).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TipoOperacao).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Usuario).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DataHora).HasDefaultValueSql("datetime('now')");
            });
        }
    }
}
