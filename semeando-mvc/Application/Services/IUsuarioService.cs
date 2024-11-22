using semeando_mvc.Application.Dtos;

namespace semeando_mvc.Application.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync(); // Obter todos os usuários
    Task<UsuarioDto> GetUsuarioByIdAsync(int id);        // Obter um usuário por ID
    Task AddUsuarioAsync(UsuarioDto usuarioDto);         // Adicionar um novo usuário
    Task UpdateUsuarioAsync(UsuarioDto usuarioDto);      // Atualizar um usuário
    
    Task DeleteUsuarioAsync(int id);

    
    
    Task DeleteBioAsync(int id);

}
