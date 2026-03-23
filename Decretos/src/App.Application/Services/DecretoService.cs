using App.Application.Events;
using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Mappers;
using App.Domain;
using App.Domain.Exceptions;
using App.Infrastructure.Messaging;
using FluentValidation;
using MassTransit;

namespace App.Application.Services;

public class DecretoService : IDecretoService
{
    private readonly IDecretoRepository _repository;
    private readonly IPublishEndpoint _publish; 
    private readonly IValidator<CriarDecretoDto> _validatorCriacaoDecreto;
    private readonly IValidator<AtualizarDecretoDto> _validatorEdicaoDecreto;


    public DecretoService(IDecretoRepository repository, IValidator<CriarDecretoDto> validatorCriacaoDecreto, 
        IValidator<AtualizarDecretoDto> validatorEdicaoDecreto, IPublishEndpoint publish )
    {
        _repository = repository;
        _validatorCriacaoDecreto = validatorCriacaoDecreto;
        _validatorEdicaoDecreto = validatorEdicaoDecreto; 
        _publish = publish; 
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

    public async Task<(List<DecretosDto>, int)> ListarDecretos(int page, int pageSize)
    {
        var (decretos, total) = await _repository.ListarDecretos(page, pageSize);

        return (DecretoMapper.ParaListadecretosDto(decretos), total);
    }

    public async Task<DecretosDto> AdicionarDecretos(CriarDecretoDto dto)
    {
        var validator = await _validatorCriacaoDecreto.ValidateAsync(dto);

        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);

        var novoDecreto = new Decreto(
            dto.Solicitante,
            dto.DataParaUso,
            dto.Secretaria,
            dto.Justificativa,
            dto.UsuarioId  
        );
        
        var novo = await _repository.AdicionarDecreto(novoDecreto);

        var decretoDto = DecretoMapper.ParaDecretosDto(novo);
        //caso e erro aqui!!
        await _publish.Publish(
            new DecretoCriadoEvent
            {
                NumeroDecreto = decretoDto.NumeroDecreto,
                Solicitante = decretoDto.Solicitante,
                DataParaUso = decretoDto.DataParaUso
            });
        return decretoDto;
    }

    public async Task<DecretosDto> EditarDecreto(int numeroDecreto, AtualizarDecretoDto decreto)
    {
        var buscarDecreto = await _repository.BuscarViaNumero(numeroDecreto);
        if (buscarDecreto == null)
            throw new NotFoundException("Decreto não encontrado.");

       var validator = await _validatorEdicaoDecreto.ValidateAsync(decreto);

        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);
        
        buscarDecreto.AlterarSolicitante(decreto.Solicitante);
        buscarDecreto.AlterarDataParaUso(decreto.DataParaUso);
        buscarDecreto.AlterarSecretaria(decreto.Secretaria);
        buscarDecreto.AlterarJustificativa(decreto.Justificativa);

        await _repository.EditarDecreto(buscarDecreto);

        return DecretoMapper.ParaDecretosDto(buscarDecreto);
    }
}