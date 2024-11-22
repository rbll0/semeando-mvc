using Microsoft.EntityFrameworkCore;
using semeando_mvc.Infrastructure.Data.Context;
using semeando_mvc.Infrastructure.Interfaces;
using semeando_mvc.Models;

namespace semeando_mvc.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.tb_Usuarios.ToListAsync();
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.tb_Usuarios.FindAsync(id);
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.tb_Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.tb_Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.tb_Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.tb_Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task DeleteBioAsync(int id)
    {
        var usuario = await _context.tb_Usuarios.FindAsync(id);
        if (usuario != null)
        {
            usuario.Bio = null; // Remove a bio do usuário
            _context.tb_Usuarios.Update(usuario); // Atualiza o registro no banco
            await _context.SaveChangesAsync();
        }
    }


}
