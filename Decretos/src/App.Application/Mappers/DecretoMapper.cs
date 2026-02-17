using App.Domain;

namespace App.Application.Mappers;

public static class DecretoMapper
{
    public static DecretosDto ParaDecretosDto(Decreto decreto)
    {
        return new DecretosDto
        {
            Id = decreto.Id,
            NumeroDecreto = decreto.NumeroDecreto,
            Solicitante = decreto.Solicitante,
            DataSolicitacao = decreto.DataSolicitacao,
            DataParaUso = decreto.DataParaUso,
            Secretaria = decreto.Secretaria,
            Justificativa = decreto.Justificativa,
            UsuarioId = decreto.UsuarioId
        };
    }

    public static List<DecretosDto> ParaListadecretosDto(IEnumerable<Decreto> decretos)
    {
        return decretos.Select(ParaDecretosDto).ToList();
    }
}