using System.ComponentModel.DataAnnotations;

namespace Auditoria.Models
{
    /// <summary>
    /// Representa uma trilha de auditoria para rastreamento de mudanças
    /// </summary>
    public class TrilhaAuditoria
    {
        /// <summary>
        /// Identificador único da trilha
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da entidade que foi afetada
        /// </summary>
        [Required(ErrorMessage = "A entidade afetada é obrigatória")]
        [StringLength(100, ErrorMessage = "A entidade deve ter no máximo 100 caracteres")]
        public string EntidadeAfetada { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de operação realizada (CREATE, UPDATE, DELETE)
        /// </summary>
        [Required(ErrorMessage = "O tipo de operação é obrigatório")]
        [StringLength(20, ErrorMessage = "O tipo de operação deve ter no máximo 20 caracteres")]
        public string TipoOperacao { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora da operação
        /// </summary>
        public DateTime DataHora { get; set; } = DateTime.Now;

        /// <summary>
        /// Usuário que realizou a operação
        /// </summary>
        [Required(ErrorMessage = "O usuário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo 100 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Dados anteriores (JSON)
        /// </summary>
        public string? DadosAnteriores { get; set; }

        /// <summary>
        /// Dados novos (JSON)
        /// </summary>
        public string? DadosNovos { get; set; }
    }
}
