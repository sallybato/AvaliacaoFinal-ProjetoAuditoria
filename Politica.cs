using System.ComponentModel.DataAnnotations;

namespace Auditoria.Models
{
    /// <summary>
    /// Representa uma política de controle interno da organização
    /// </summary>
    public class Politica
    {
        /// <summary>
        /// Identificador único da política
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da política
        /// </summary>
        [Required(ErrorMessage = "O nome da política é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada da política
        /// </summary>
        [Required(ErrorMessage = "A descrição da política é obrigatória")]
        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação da política
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        /// <summary>
        /// Indica se a política está ativa
        /// </summary>
        public bool Ativa { get; set; } = true;
    }
}
