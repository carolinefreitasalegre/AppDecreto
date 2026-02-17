using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Mappers;
using App.Domain;
using App.Domain.Exceptions;

namespace App.Application.Services;

public class DecretoService : IDecretoService
{
    private readonly IDecretoRepository _repository;

    public DecretoService(IDecretoRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<DecretosDto> BuscarViaNumero(int numero)
    {
        var decreto = await _repository.BuscarViaNumero(numero);
        if (decreto == null)
            throw new NotFoundException("Número de Decreto não encontrado.");

        return DecretoMapper.ParaDecretosDto(decreto);
    }

    public async Task<DecretosDto> BuscarViaId(int id)
    {
        var decreto = await _repository.BuscarViaId(id);
        if (decreto == null)
            throw new NotFoundException("Decreto não encontrado.");

        return DecretoMapper.ParaDecretosDto(decreto);
    }

    public async Task<List<DecretosDto>> ListarDecretos()
    {
        var decretos = await _repository.ListarDecretos();

        return DecretoMapper.ParaListadecretosDto(decretos);
    }

    public async Task<DecretosDto> AdicionarDecretos(CriarDecretoDto dto)
    {
        var verificarDecreto = await _repository.BuscarViaNumero(dto.NumeroDecreto);
        if (verificarDecreto != null)
            throw new BusinessException("Número de Decreto já consta no Banco de Dados.");

        var novoDecreto = new Decreto(
            dto.NumeroDecreto,
            dto.Solicitante,
            dto.DataParaUso,
            dto.Secretaria,
            dto.Justificativa,
            dto.UsuarioId  
        );

        var novo = await _repository.AdicionarDecreto(novoDecreto);

        return DecretoMapper.ParaDecretosDto(novo);
    }

    public async Task<DecretosDto> EditarDecreto(AtualizarDecretoDto decreto, int id)
    {
        var buscarDecreto = await _repository.BuscarViaId(id);
        if (buscarDecreto == null)
            throw new NotFoundException("Decreto não encontrado.");

        if (buscarDecreto.NumeroDecreto != decreto.NumeroDecreto)
        {
            var decretoJaExiste = await _repository.BuscarViaNumero(decreto.NumeroDecreto);
            if (decretoJaExiste != null)
                throw new BusinessException("Decreto já cadastrado.");
        }
        
        buscarDecreto.AlterarNumero(decreto.NumeroDecreto);
        buscarDecreto.AlterarSolicitante(decreto.Solicitante);
        buscarDecreto.AlterarDataParaUso(decreto.DataParaUso);
        buscarDecreto.AlterarSecretaria(decreto.Secretaria);
        buscarDecreto.AlterarJustificativa(decreto.Justificativa);

        await _repository.EditarDecreto(buscarDecreto, id);

        return DecretoMapper.ParaDecretosDto(buscarDecreto);
    }
}