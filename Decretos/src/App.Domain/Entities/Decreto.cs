using App.Domain.Enums;

namespace App.Domain;

public class Decreto
{
        public int Id { get; private set; }                        
        public int NumeroDecreto { get; private set; }           
        public string Solicitante { get; private set; } = null!;
        public DateTime DataSolicitacao { get; private set; } = DateTime.UtcNow;      
        public DateTime DataParaUso { get; private set; }           
        public Secretaria Secretaria { get; private set; }          
        public string Justificativa { get; private set; } = null!;  
        
        public Usuario Usuario { get; private set; } = null!;
        public int UsuarioId { get; private set; }

        
        public Decreto()
        {
        }

        public Decreto( 
                string solicitante,
                DateTime dataParaUso,
                Secretaria secretaria,
                string justificativa,
                int usuarioId)
        {
                ValidarSolicitante(solicitante);
                ValidarDatas(dataParaUso);
                ValidarJustificativa(justificativa);
                ValidarUsuario(usuarioId);

                Solicitante = solicitante;
                DataSolicitacao = DateTime.UtcNow;
                DataParaUso = dataParaUso;
                Secretaria = secretaria;
                Justificativa = justificativa;
                UsuarioId = usuarioId;
        }

        public void AlterarDataParaUso(DateTime novaData)
        {
                ValidarDatas(novaData);
                DataParaUso = novaData;
        }

        public void AlterarJustificativa(string justificativa)
        {
                ValidarJustificativa(justificativa);
                Justificativa = justificativa;
        }
        
        public void AlterarSolicitante(string novoSolicitante)
        {
                ValidarSolicitante(novoSolicitante);
                Solicitante = novoSolicitante;
        }

        public void AlterarSecretaria(Secretaria novaSecretaria)
        {
                Secretaria = novaSecretaria;
        }

        
        private static void ValidarSolicitante(string solicitante)
        {
                if (string.IsNullOrWhiteSpace(solicitante))
                        throw new Exception("Solicitante é obrigatório.");
        }

        private static void ValidarDatas(DateTime dataParaUso)
        {
                if (dataParaUso.Date < DateTime.UtcNow.Date)
                        throw new Exception("Data para uso não pode ser no passado.");
        }

        private static void ValidarJustificativa(string justificativa)
        {
                if (string.IsNullOrWhiteSpace(justificativa))
                        throw new Exception("Justificativa é obrigatória.");
        }

        private static void ValidarUsuario(int usuarioId)
        {
                if (usuarioId <= 0)
                        throw new Exception("Usuário inválido.");
        }
}