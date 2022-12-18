namespace Utilities.Authentication;

public class JwtOptions
{
    public string SigningKey { get; set; } = null!;
    public int ExpireSeconds { get; set; }
}