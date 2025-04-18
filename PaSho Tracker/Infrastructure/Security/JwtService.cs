using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PaSho_Tracker.Services;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IConfiguration config, ILogger<JwtService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public string GenerateToken(string userId, string email, IList<string> roles)
    {
        try
        {
            _logger.LogInformation("Generating JWT for user {UserId} with email {Email}", userId, email);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("JWT successfully generated for user {UserId}", userId);
            return jwt;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "JWT generation failed for user {UserId}", userId);
            return null;
        }
    }
}