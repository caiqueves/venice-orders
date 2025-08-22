using System.Security.Cryptography;
using System.Text;

namespace Venice.Orders.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty; // senha já com hash

    private User() { }

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        PasswordHash = HashPassword(password);
    }

    private static string HashPassword(string password)
    {
        // Gera um salt aleatório
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // PBKDF2: 100.000 iterações, SHA256
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        // Combina salt + hash em um único array
        byte[] hashBytes = new byte[48]; // 16 + 32
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Retorna em base64
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password)
    {
        byte[] hashBytes = Convert.FromBase64String(PasswordHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        for (int i = 0; i < 32; i++)
            if (hashBytes[i + 16] != hash[i])
                return false;

        return true;
    }
}
