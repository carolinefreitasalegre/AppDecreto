using System.Text.Json.Serialization;
using App.Domain.Enums;
using App.Domain.Exceptions;

namespace App.Domain;

public class Usuario
{
    public int Id { get; private set; }                 
    public int Matricula { get; private set; }           
    public string Nome { get; private set; } = null!;    
    public string Email { get; private set; } = null!;   
    public string Senha { get; private set; } = null!;   
    public UsuarioRole Role { get; private set; }   
    public UsuarioStatus Status { get; private set; } 
    public DateTime CriadoEm { get; private set; }
    [JsonIgnore]
    public ICollection<Decreto> Decretos { get; private set; } = new List<Decreto>();

    protected Usuario(){}

    public Usuario(int matricula, string nome, string email, string senha)
    {
        ValidarMatricula(matricula);
        ValidarNome(nome);
        ValidarEmail(email);
        ValidarSenha(senha);
        

        Matricula = matricula;
        Nome = nome;
        Email = email;
        Role = UsuarioRole.USUARIO;
        Status = UsuarioStatus.ATIVO;
        CriadoEm = DateTime.UtcNow;

        DefinirSenha(senha);
    }

    public void AlterarNome(string nome)
    {
        ValidarNome(nome);
        Nome = nome;
    }

    public void AlterarEmail(string email)
    {
        ValidarEmail(email);
        Email = email;
    }

    public void AlterarStatus(UsuarioStatus status)
    {
        Status = status;
    }

    public void AlterarRole(UsuarioRole role)
    {
        Role = role;
    }

    public void AlterarSenha(string novaSenha)
    {
        ValidarSenha(novaSenha);
        DefinirSenha(novaSenha);
    }

    public void AlterarMatricula(int matricula)
    {
        ValidarMatricula(matricula);
        Matricula = matricula;
    }
    private void DefinirSenha(string senha)
    {
        Senha = senha;
    }

    private static void ValidarMatricula(int matricula)
    {
        if (matricula <= 0)
        {
            throw new DomainException("Matrícula inválida");
        }
    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("Nome é obrigatório.");
        }
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainException("Email inválido.");
    }

    private static void ValidarSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            throw new DomainException("Senha deve ter ao menos 6 caracteres.");
    }
}