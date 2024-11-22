using semeando_mvc.Application.Dtos;
using semeando_mvc.Infrastructure.Interfaces;
using semeando_mvc.Models;

namespace semeando_mvc.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    // Obtém todos os usuários
    public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            IdUsuario = u.IdUsuario,
            NomeUsuario = u.NomeUsuario,
            Email = u.Email,
            Bio = u.Bio ?? "Sem bio definida" // Define valor padrão se nulo
        });
    }

    // Obtém um usuário pelo ID
    public async Task<UsuarioDto> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            NomeUsuario = usuario.NomeUsuario,
            Email = usuario.Email,
            Bio = usuario.Bio ?? "Sem bio definida" // Define valor padrão se nulo
        };
    }

    // Adiciona um novo usuário
    public async Task AddUsuarioAsync(UsuarioDto usuarioDto)
    {
        if (string.IsNullOrWhiteSpace(usuarioDto.NomeUsuario) || string.IsNullOrWhiteSpace(usuarioDto.Email))
        {
            throw new ArgumentException("Nome de usuário e email são obrigatórios.");
        }

        var usuario = new Usuario
        {
            NomeUsuario = usuarioDto.NomeUsuario,
            Email = usuarioDto.Email,
            Bio = usuarioDto.Bio ?? "Sem bio definida", // Define valor padrão se nulo
            Ranking = usuarioDto.Ranking ?? "C", // Valor padrão para ranking
            Streak = usuarioDto.Streak ?? 0 // Valor padrão para streak
        };

        await _usuarioRepository.AddAsync(usuario);
    }

    // Atualiza um usuário existente
    public async Task UpdateUsuarioAsync(UsuarioDto usuarioDto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioDto.IdUsuario);
        if (usuario == null)
        {
            throw new KeyNotFoundException($"Usuário com ID {usuarioDto.IdUsuario} não encontrado.");
        }

        usuario.NomeUsuario = usuarioDto.NomeUsuario;
        usuario.Email = usuarioDto.Email;
        usuario.Bio = usuarioDto.Bio ?? usuario.Bio; // Mantém o valor atual se nulo
        usuario.Ranking = usuarioDto.Ranking ?? usuario.Ranking; // Mantém o valor atual se nulo
        usuario.Streak = usuarioDto.Streak ?? usuario.Streak; // Mantém o valor atual se nulo

        await _usuarioRepository.UpdateAsync(usuario);
    }

    // Deleta um usuário pelo ID
    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
        {
            throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
        }

        await _usuarioRepository.DeleteAsync(id);
    }
    
    public async Task DeleteBioAsync(int id)
    {
        await _usuarioRepository.DeleteBioAsync(id);
    }
}
