using App.Domain;

namespace App.Application.Interfaces.Repository;

public interface IDecretoRepository
{
    Task<Decreto?>BuscarViaNumero(int numero);
    Task<int?>BuscarUltimoDecreto();
    Task<Decreto?>BuscarViaId(int id);
    Task<List<Decreto>>ListarDecretos();
    Task<Decreto> AdicionarDecreto(Decreto decreto);
    Task<Decreto> EditarDecreto(Decreto decreto);
}