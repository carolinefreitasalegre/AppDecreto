using App.Application.Interfaces.Repository;
using App.Domain;
using App.Infrastructure.Data.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> BuscarViaEmail(string email)
    {
       return await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<Usuario?> BuscarViaId(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Usuario?> BuscarViaMatricula(int matricula)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(e => e.Matricula == matricula);
    }

    public async Task<List<Usuario>> ListarUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> AdicionarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario> EditarUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
}