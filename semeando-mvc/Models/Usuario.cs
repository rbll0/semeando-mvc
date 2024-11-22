using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace semeando_mvc.Models;

[Table("TB_USUARIO", Schema = "RM553326")] // Ajuste o schema se necessário
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID_USUARIO")]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("NOME_USUARIO")]
    public string NomeUsuario { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("EMAIL")]
    public string Email { get; set; }

    [MaxLength(1)]
    [Column("RANKING")]
    public string? Ranking { get; set; } // Permitir valores nulos

    [Column("STREAK")]
    public int? Streak { get; set; } = 0; // Permitir valores nulos com valor padrão

    [MaxLength(255)]
    [Column("BIO")]
    public string? Bio { get; set; } // Permitir valores nulos
}
