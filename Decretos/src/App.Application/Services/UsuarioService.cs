using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Mappers;
using App.Domain;
using App.Domain.Exceptions;
using FluentValidation;

namespace App.Application.Services;

public class UsuarioService : IUsuarioService
    
{
    private readonly IUsuarioRepository _repository;
    private readonly IHashSenhaService _hashService;
    private readonly IValidator<CriarUsuarioDto> _validatorCriacaoUser;
    private readonly IValidator<AtualizarUsuarioDto> _validatorEdicaoUser;

    public UsuarioService(IUsuarioRepository repository, IHashSenhaService hashService, IValidator<CriarUsuarioDto> validatorCriacaoUser,IValidator<AtualizarUsuarioDto> validatorEdicaoUser)
    {
        _repository = repository;
        _hashService = hashService;
        _validatorCriacaoUser = validatorCriacaoUser;
        _validatorEdicaoUser = validatorEdicaoUser;
    }

    public async Task<UsuariosDto> BuscarPorEmail(string email)
    {
        var usuario = await _repository.BuscarViaEmail(email);
        if(usuario == null)
            throw new NotFoundException("Usuáio não encontrado.");

        return UsuarioMapper.ParaUsuarioDto(usuario);
    }

    public async Task<UsuariosDto> BuscarPorId(int id)
    {
        var usuario = await _repository.BuscarViaId(id);
        if (usuario == null)
            throw new NotFoundException("Usuáio não encontrado.");

        return UsuarioMapper.ParaUsuarioDto(usuario);
    }

    public async Task<UsuariosDto> BuscarPorMatricula(int matricula)
    {
        var usuario = await _repository.BuscarViaMatricula(matricula);
        if(usuario == null)
            throw new NotFoundException("Usuáio não encontrado.");

        return UsuarioMapper.ParaUsuarioDto(usuario);
    }

    public async Task<List<UsuariosDto>> Listar()
    {
       var usuarios = await _repository.ListarUsuarios();

       return UsuarioMapper.ParaListaUsuarioDto(usuarios);
    }

    public async Task<UsuariosDto> CriarUsuario(CriarUsuarioDto dto)
    {
        var validator = await _validatorCriacaoUser.ValidateAsync(dto);
        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);
     
        var senhaHash = _hashService.GerarHash(dto.Senha);

        var usuario = new Usuario(
            dto.Matricula,
            dto.Nome,
            dto.Email,
            senhaHash
        );
        await _repository.AdicionarUsuario(usuario);
          
        return UsuarioMapper.ParaUsuarioDto(usuario);
    }

    public async Task<UsuariosDto> EditarUsuario(AtualizarUsuarioDto dto)
    {
        var usuario = await _repository.BuscarViaId(dto.Id);
        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.");

        var validator = await _validatorEdicaoUser.ValidateAsync(dto);
        if (!validator.IsValid)
            throw new BusinessException(validator.Errors.First().ErrorMessage);
        
        usuario.AlterarNome(dto.Nome);
        usuario.AlterarEmail(dto.Email);
        usuario.AlterarRole(dto.Role);
        usuario.AlterarStatus(dto.Status);
        usuario.AlterarMatricula(dto.Matricula);
        
       await _repository.EditarUsuario(usuario);

       return UsuarioMapper.ParaUsuarioDto(usuario);

    }

    public async Task AlterarSenha(int id, string senha)
    {
        var usuario = await _repository.BuscarViaId(id);
        if (usuario == null)
            throw new NotFoundException("Usuário não encontrado.");

        var senhaHash = _hashService.GerarHash(senha);
        usuario.AlterarSenha(senhaHash);

        _repository.EditarUsuario(usuario);
    }
}









