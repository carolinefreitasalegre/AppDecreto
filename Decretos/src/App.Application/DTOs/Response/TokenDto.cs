namespace App.Application;

public class TokenDto
{
    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}