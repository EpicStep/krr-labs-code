using System.Text.RegularExpressions;

namespace UsersApi.Data;

// Тело запроса POST/PUT: {"Login": "...", "PassHash": "..."}
public record UserRequest(string? Login, string? PassHash)
{
    private static readonly Regex LoginRegex = new("^[a-zA-Z0-9_]{3,50}$");
    // Ожидается SHA-256 в виде 64 шестнадцатеричных символов
    private static readonly Regex HashRegex = new("^[a-fA-F0-9]{64}$");

    public string? Validate()
    {
        if (string.IsNullOrWhiteSpace(Login) || !LoginRegex.IsMatch(Login))
            return "Логин должен содержать от 3 до 50 латинских букв, цифр или _";
        if (string.IsNullOrWhiteSpace(PassHash) || !HashRegex.IsMatch(PassHash))
            return "PassHash должен быть SHA-256 хешем (64 hex-символа), а не открытым паролем";
        return null;
    }
}

// В ответах хеш пароля не возвращается
public record UserResponse(int Id, string Login)
{
    public static UserResponse From(User u) => new(u.Id, u.Login);
}

public record MessageResponse(string Message);
