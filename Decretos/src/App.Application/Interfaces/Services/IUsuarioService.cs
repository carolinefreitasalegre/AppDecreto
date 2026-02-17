using App.Domain;

namespace App.Application.Interfaces;

public interface IUsuarioService
{
        Task<UsuariosDto> BuscarPorEmail(string email);
        Task<UsuariosDto> BuscarPorId(int id);
        Task<UsuariosDto> BuscarPorMatricula(int matricula);
        Task<List<UsuariosDto>> Listar();
        Task<UsuariosDto> CriarUsuario(CriarUsuarioDto dto);
        Task<UsuariosDto> EditarUsuario(AtualizarUsuarioDto dto);
        Task AlterarSenha(int id, string senha);
}