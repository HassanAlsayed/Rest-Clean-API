using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;
using HR.LeaveManagmant.Domain.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagmant.Persistence.Token;

public class TokenMethode
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshToken _refreshToken;
    private readonly IMediator _mediator;

    public TokenMethode(IConfiguration configuration,IRefreshToken refreshToken,IMediator mediator)
    {
        _configuration = configuration;
        _refreshToken = refreshToken;
        _mediator = mediator;
    }
    public async Task<TokenRequest> GenerateToken(IdentityUser user)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT_SECRET_KEY"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),

                }),

            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["ExpiryTimeFrame"]!)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = tokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            Token = GenerateRandomString(50), //generate refresh token,
            UserId = Guid.Parse(user.Id),
            JwtId = Guid.Parse(token.Id),
            AddedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6),
            IdRevoked = false,
            IdUsed = false,
        };

        await _mediator.Send(new RefreshTokenCommand(refreshToken));

        return new TokenRequest { Token = jwtToken, Refreshtoken = refreshToken.Token };
    }

    private string GenerateRandomString(int length)
    {
        var rnd = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
    }

    public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
    }


}
