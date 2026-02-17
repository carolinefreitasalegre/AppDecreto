using App.Domain;

namespace App.Application.Mappers;

public static class UsuarioMapper
{
    public static UsuariosDto ParaUsuarioDto(Usuario usuario)
    {
        return new UsuariosDto
        {
            Id = usuario.Id,
            Matricula = usuario.Matricula,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role,
            Status = usuario.Status,
            CriadoEm = usuario.CriadoEm
        };
    }

    public static List<UsuariosDto> ParaListaUsuarioDto(IEnumerable<Usuario> usuarios)
    {
        return usuarios.Select(ParaUsuarioDto).ToList();
    }
    
}