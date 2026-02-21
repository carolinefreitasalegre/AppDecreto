using App.Domain;

namespace App.Application.Interfaces.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?>BuscarViaEmail(string email);
    Task<Usuario?>BuscarViaId(int id);
    Task<Usuario?>BuscarViaMatricula(int matricula);
    Task<List<Usuario>>ListarUsuarios();
    Task<Usuario> AdicionarUsuario(Usuario usuario);
    Task<Usuario> EditarUsuario(Usuario usuario);
}