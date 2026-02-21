using App.Application.Interfaces.Repository;
using App.Domain;
using App.Infrastructure.Data.DbConnection;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class DecretoRepository : IDecretoRepository
{
    private readonly AppDbContext _context;

    public DecretoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Decreto?> BuscarViaNumero(int numero)
    {
        return await _context.Decretos.FirstOrDefaultAsync(e => e.NumeroDecreto == numero);
    }

    public async Task<int?> BuscarUltimoDecreto()
    {
        return await _context.Decretos.OrderByDescending(d => d.NumeroDecreto)
            .Select(d => d.NumeroDecreto).FirstOrDefaultAsync();
    }

    public async Task<Decreto?> BuscarViaId(int id)
    {
        return await _context.Decretos.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Decreto>> ListarDecretos()
    {
        return await _context.Decretos
            .Include(d => d.Usuario).ToListAsync();
    }

    public async Task<Decreto> AdicionarDecreto(Decreto decreto)
    {
        _context.Decretos.Add(decreto);
        await _context.SaveChangesAsync();

        return decreto;
    }

    public async Task<Decreto> EditarDecreto(Decreto decreto)
    {
        _context.Decretos.Update(decreto);
        await _context.SaveChangesAsync();

        return decreto;
    }
}