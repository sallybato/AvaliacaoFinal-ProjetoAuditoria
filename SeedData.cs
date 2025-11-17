using Auditoria.Models;

namespace Auditoria.Data
{
    public static class SeedData
    {
        public static void Initialize(ControleInternoContext context)
        {
            // Verificar se já existem dados
            if (context.Politicas.Any() || context.Permissoes.Any() || 
                context.LogsAcesso.Any() || context.TrilhasAuditoria.Any())
            {
                return; // Banco já foi populado
            }

            // Criar políticas
            var politicas = new List<Politica>
            {
                new Politica { Nome = "Política de Senhas", Descricao = "Define requisitos mínimos para criação de senhas seguras", DataCriacao = DateTime.Now.AddDays(-30), Ativa = true },
                new Politica { Nome = "Política de Acesso Remoto", Descricao = "Regulamenta o acesso remoto aos sistemas corporativos", DataCriacao = DateTime.Now.AddDays(-25), Ativa = true },
                new Politica { Nome = "Política de Backup", Descricao = "Estabelece procedimentos para backup de dados críticos", DataCriacao = DateTime.Now.AddDays(-20), Ativa = true },
                new Politica { Nome = "Política de Antivírus", Descricao = "Define uso obrigatório de software antivírus atualizado", DataCriacao = DateTime.Now.AddDays(-15), Ativa = true },
                new Politica { Nome = "Política de Firewall", Descricao = "Configuração e manutenção de firewalls corporativos", DataCriacao = DateTime.Now.AddDays(-10), Ativa = true },
                new Politica { Nome = "Política de Criptografia", Descricao = "Uso de criptografia para dados sensíveis", DataCriacao = DateTime.Now.AddDays(-5), Ativa = true },
                new Politica { Nome = "Política de Auditoria", Descricao = "Procedimentos para auditoria de sistemas", DataCriacao = DateTime.Now.AddDays(-2), Ativa = true },
                new Politica { Nome = "Política de Incidentes", Descricao = "Tratamento de incidentes de segurança", DataCriacao = DateTime.Now.AddDays(-1), Ativa = true },
                new Politica { Nome = "Política de Treinamento", Descricao = "Capacitação em segurança da informação", DataCriacao = DateTime.Now, Ativa = true },
                new Politica { Nome = "Política de Controle de Acesso", Descricao = "Gestão de permissões e acessos", DataCriacao = DateTime.Now, Ativa = false }
            };

            // Criar permissões
            var permissoes = new List<Permissao>
            {
                new Permissao { Nome = "Administrador", Nivel = 5, Descricao = "Acesso total ao sistema" },
                new Permissao { Nome = "Gerente", Nivel = 4, Descricao = "Acesso gerencial com algumas restrições" },
                new Permissao { Nome = "Supervisor", Nivel = 3, Descricao = "Acesso de supervisão limitado" },
                new Permissao { Nome = "Operador", Nivel = 2, Descricao = "Acesso operacional básico" },
                new Permissao { Nome = "Visualizador", Nivel = 1, Descricao = "Apenas visualização de dados" },
                new Permissao { Nome = "Auditor", Nivel = 4, Descricao = "Acesso para auditoria e relatórios" },
                new Permissao { Nome = "Suporte", Nivel = 3, Descricao = "Acesso para suporte técnico" },
                new Permissao { Nome = "Desenvolvedor", Nivel = 4, Descricao = "Acesso para desenvolvimento" },
                new Permissao { Nome = "Analista", Nivel = 3, Descricao = "Acesso para análise de dados" },
                new Permissao { Nome = "Consultor", Nivel = 2, Descricao = "Acesso consultivo limitado" }
            };

            // Criar logs de acesso
            var logsAcesso = new List<LogAcesso>();
            var usuarios = new[] { "admin", "gerente1", "supervisor1", "operador1", "auditor1", "suporte1", "dev1", "analista1", "consultor1", "user1" };
            var acoes = new[] { "Login", "Logout", "Visualizar Relatório", "Criar Usuário", "Editar Política", "Excluir Registro", "Exportar Dados", "Configurar Sistema", "Backup", "Auditoria" };
            var ips = new[] { "192.168.1.10", "192.168.1.11", "192.168.1.12", "10.0.0.5", "10.0.0.6", "172.16.0.1", "172.16.0.2", "203.0.113.1", "203.0.113.2", "198.51.100.1" };

            for (int i = 0; i < 50; i++)
            {
                logsAcesso.Add(new LogAcesso
                {
                    Usuario = usuarios[Random.Shared.Next(usuarios.Length)],
                    Acao = acoes[Random.Shared.Next(acoes.Length)],
                    DataHora = DateTime.Now.AddHours(-Random.Shared.Next(720)), // Últimos 30 dias
                    IpOrigem = ips[Random.Shared.Next(ips.Length)]
    
                });
            }

            // Criar trilhas de auditoria
            var trilhasAuditoria = new List<TrilhaAuditoria>();
            var entidades = new[] { "Politica", "Permissao", "Usuario", "Sistema", "Configuracao", "Relatorio", "Backup", "Log" };
            var operacoes = new[] { "CREATE", "UPDATE", "DELETE", "READ" };

            for (int i = 0; i < 50; i++)
            {
                trilhasAuditoria.Add(new TrilhaAuditoria
                {
                    EntidadeAfetada = entidades[Random.Shared.Next(entidades.Length)],
                    TipoOperacao = operacoes[Random.Shared.Next(operacoes.Length)],
                    DataHora = DateTime.Now.AddHours(-Random.Shared.Next(720)),
                    Usuario = usuarios[Random.Shared.Next(usuarios.Length)],
                    DadosAnteriores = $"{{\"campo\": \"valor_anterior_{i}\"}}",
                    DadosNovos = $"{{\"campo\": \"valor_novo_{i}\"}}"
                });
            }

            // Adicionar ao contexto
            context.Politicas.AddRange(politicas);
            context.Permissoes.AddRange(permissoes);
            context.LogsAcesso.AddRange(logsAcesso);
            context.TrilhasAuditoria.AddRange(trilhasAuditoria);

            // Salvar no banco
            context.SaveChanges();
        }
    }
}
