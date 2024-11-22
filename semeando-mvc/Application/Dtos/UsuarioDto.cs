using System.ComponentModel.DataAnnotations;

namespace semeando_mvc.Application.Dtos;

public class UsuarioDto
{
    public int IdUsuario { get; set; } // ID único do usuário

    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome do usuário não pode exceder 100 caracteres.")]
    public string NomeUsuario { get; set; } // Nome do usuário

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um email válido.")]
    [MaxLength(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
    public string Email { get; set; } // Email do usuário

    [MaxLength(255, ErrorMessage = "A bio não pode exceder 255 caracteres.")]
    public string? Bio { get; set; } // Bio opcional do usuário

    [MaxLength(1, ErrorMessage = "O ranking deve ter apenas um caractere.")]
    public string? Ranking { get; set; } // Ranking opcional do usuário

    public int? Streak { get; set; } // Streak opcional do usuário
}
