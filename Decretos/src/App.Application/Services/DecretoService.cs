using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Mappers;
using App.Domain;
using App.Domain.Exceptions;
using FluentValidation;

namespace App.Application.Services;

public class DecretoService : IDecretoService
{
    private readonly IDecretoRepository _repository;
    private readonly IValidator<CriarDecretoDto> _validatorCriacaoDecreto;
    private readonly IValidator<AtualizarDecretoDto> _validatorEdicaoDecreto;


    public DecretoService(IDecretoRepository repository, IValidator<CriarDecretoDto> validatorCriacaoDecreto, IValidator<AtualizarDecretoDto> validatorEdicaoDecreto)
    {
        _repository = repository;
        _validatorCriacaoDecreto = validatorCriacaoDecreto;
        _validatorEdicaoDecreto = validatorEdicaoDecreto;
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
        var validator = await _validatorCriacaoDecreto.ValidateAsync(dto);

        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);

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

    public async Task<DecretosDto> EditarDecreto(AtualizarDecretoDto decreto)
    {
        var buscarDecreto = await _repository.BuscarViaId(decreto.Id);
        if (buscarDecreto == null)
            throw new NotFoundException("Decreto não encontrado.");

        // if (buscarDecreto.NumeroDecreto != decreto.NumeroDecreto)
        // {
        //     var decretoJaExiste = await _repository.BuscarViaNumero(decreto.NumeroDecreto);
        //     if (decretoJaExiste != null)
        //         throw new BusinessException("Decreto já cadastrado.");
        // }
        var validator = await _validatorEdicaoDecreto.ValidateAsync(decreto);

        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);
        
        buscarDecreto.AlterarNumero(decreto.NumeroDecreto);
        buscarDecreto.AlterarSolicitante(decreto.Solicitante);
        buscarDecreto.AlterarDataParaUso(decreto.DataParaUso);
        buscarDecreto.AlterarSecretaria(decreto.Secretaria);
        buscarDecreto.AlterarJustificativa(decreto.Justificativa);

        await _repository.EditarDecreto(buscarDecreto);

        return DecretoMapper.ParaDecretosDto(buscarDecreto);
    }
}