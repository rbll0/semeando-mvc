using semeando_mvc.Models;

namespace semeando_mvc.Infrastructure.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();       // Obter todos os usuários
    Task<Usuario> GetByIdAsync(int id);             // Obter usuário por ID
    Task AddAsync(Usuario usuario);                 // Adicionar um novo usuário
    Task UpdateAsync(Usuario usuario);              // Atualizar um usuário
    Task DeleteAsync(int id);                       // Deletar um usuário por ID
    
    Task DeleteBioAsync(int id);

}

