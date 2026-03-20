namespace App.Application.Interfaces;

public interface IDecretoService
{
    Task<DecretosDto>BuscarViaNumero(int numero);
    Task<DecretosDto>BuscarViaId(int id);
    Task<(List<DecretosDto>, int)>ListarDecretos(int page, int pageSize);
    Task<DecretosDto> AdicionarDecretos(CriarDecretoDto decreto);
    Task<DecretosDto> EditarDecreto(int numeroDecreto, AtualizarDecretoDto decreto);
}