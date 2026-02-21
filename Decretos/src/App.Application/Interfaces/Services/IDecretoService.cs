namespace App.Application.Interfaces;

public interface IDecretoService
{
    Task<DecretosDto>BuscarViaNumero(int numero);
    Task<DecretosDto>BuscarViaId(int id);
    Task<List<DecretosDto>>ListarDecretos();
    Task<DecretosDto> AdicionarDecretos(CriarDecretoDto decreto);
    Task<DecretosDto> EditarDecreto(AtualizarDecretoDto decreto);
}