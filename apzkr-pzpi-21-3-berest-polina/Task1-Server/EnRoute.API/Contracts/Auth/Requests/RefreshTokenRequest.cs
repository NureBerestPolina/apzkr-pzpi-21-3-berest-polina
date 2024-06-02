namespace EnRoute.API.Contracts.Auth.Requests;

public record RefreshTokenRequest(string Token, string RefreshToken);