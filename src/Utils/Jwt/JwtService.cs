using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using ExpenseTrackerGroup3.Utils.Jwt.Interfaces;

namespace ExpenseTrackerGroup3.Utils.Jwt;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateToken(Guid userId, string email, string tokenType, TimeSpan tokenExpiration)
    {
        var code = _jwtOptions.JwtCode;

        if (code == null)
        {
            throw new ArgumentException("Jwt code not configured");
        }

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(code));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim("tokenType", tokenType),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.Add(tokenExpiration),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string ValidateToken(string token, string expectedTokenType)
    {
        var code = _jwtOptions.JwtCode;

        if (code == null)
        {
            throw new ArgumentException("Jwt code not configured");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(code);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var tokenType = jwtToken.Claims.First(x => x.Type == "tokenType").Value;
            if (tokenType != expectedTokenType)
            {
                throw new SecurityTokenException("Invalid token type");
            }

            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            return email;
        }
        catch
        {
            throw new SecurityTokenException("Invalid token");
        }
    }
}
