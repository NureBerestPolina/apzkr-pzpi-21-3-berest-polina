namespace EnRoute.API.Contracts.Auth.Responses;

public record LoginResponse(string Token, string RefreshToken);