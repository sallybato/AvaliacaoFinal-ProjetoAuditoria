using System.ComponentModel.DataAnnotations;

namespace Auditoria.Models
{
    /// <summary>
    /// Representa um log de acesso ao sistema
    /// </summary>
    public class LogAcesso
    {
        /// <summary>
        /// Identificador único do log
        /// </summary>
        public int Id { get; set; }
        
    
        /// <summary>
        /// Nome do usuário que realizou a ação
        /// </summary>
        [Required(ErrorMessage = "O usuário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo 100 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Ação realizada pelo usuário
        /// </summary>
        [Required(ErrorMessage = "A ação é obrigatória")]
        [StringLength(200, ErrorMessage = "A ação deve ter no máximo 200 caracteres")]
        public string Acao { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora da ação
        /// </summary>
        public DateTime DataHora { get; set; } = DateTime.Now;

        /// <summary>
        /// Endereço IP de origem da ação
        /// </summary>
        [Required(ErrorMessage = "O IP de origem é obrigatório")]
        [StringLength(45, ErrorMessage = "O IP deve ter no máximo 45 caracteres")]
        public string IpOrigem { get; set; } = string.Empty;
    }
}
