using System.ComponentModel.DataAnnotations;

namespace Auditoria.Models
{
    /// <summary>
    /// Representa uma permissão de acesso no sistema
    /// </summary>
    public class Permissao
    {
        /// <summary>
        /// Identificador único da permissão
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da permissão
        /// </summary>
        [Required(ErrorMessage = "O nome da permissão é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Nível de acesso (1-5, onde 5 é o mais alto)
        /// </summary>
        [Required(ErrorMessage = "O nível é obrigatório")]
        [Range(1, 5, ErrorMessage = "O nível deve estar entre 1 e 5")]
        public int Nivel { get; set; }

        /// <summary>
        /// Descrição da permissão
        /// </summary>
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; } = string.Empty;
    }
}
