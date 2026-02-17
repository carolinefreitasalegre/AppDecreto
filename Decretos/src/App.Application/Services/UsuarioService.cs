using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Mappers;
using App.Domain;
using App.Domain.Exceptions;

namespace App.Application.Services;

public class UsuarioService : IUsuarioService
    
{
    private readonly IUsuarioRepository _repository;
    private readonly IHashSenhaService _hashService;

    public UsuarioService(IUsuarioRepository repository, IHashSenhaService hashService)
    {
        _repository = repository;
        _hashService = hashService;
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
        var usuarioExisteEmail = await _repository.BuscarViaEmail(dto.Email); 
        var usuarioExisteMatricula = await _repository.BuscarViaMatricula(dto.Matricula) ;

        if (usuarioExisteEmail != null)
            throw new BusinessException("Email já registrado no banco de dados.");
        
        if(usuarioExisteMatricula != null)
            throw new BusinessException("Matrícula já registrada no banco de dados.");

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

        if (usuario.Email != dto.Email)
        {
            var emailExiste = await _repository.BuscarViaEmail(dto.Email);
            if (emailExiste != null)
                throw new BusinessException("E-mail já cadastrado.");
        }

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









