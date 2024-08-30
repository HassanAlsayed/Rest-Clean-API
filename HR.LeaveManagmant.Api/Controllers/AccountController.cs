using HR.LeaveManagmant.Application.Contracts.Persistence;
using HR.LeaveManagmant.Application.Features.LeaveType.Commands.Refresh_Token;
using HR.LeaveManagmant.Application.Features.LeaveType.Quaries.Refresh_TokenQuery;
using HR.LeaveManagmant.Domain.Entities;
using HR.LeaveManagmant.Domain.Entities.Account;
using HR.LeaveManagmant.Persistence.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace HR.LeaveManagmant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshToken _refreshToken;
        private readonly IMediator _mediator;

        public AccountController(UserManager<IdentityUser> userManager,IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters,
            IRefreshToken refreshToken,IMediator mediator)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshToken = refreshToken;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserInfo user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("please check user credentails");
            }

            // check if email already exist

            var existedEmail = await _userManager.FindByEmailAsync(user.Email);
            if(existedEmail is not null)
            {
                return BadRequest("the email is used");
            }

            // create user

            var newUser = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Name,
                EmailConfirmed = false,
            };

            var isCreated = await _userManager.CreateAsync(newUser,user.Password);

            if(isCreated.Succeeded)
            {
                return Ok("registeration sucssefully");
            }

            return BadRequest("check credentials");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("please check user credentails");
            }

            // check if the email exist
            var existingUser = await _userManager.FindByEmailAsync(loginInfo.Email);

            if(existingUser is not null)
            {

                // check the password
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginInfo.Password);

                    if(!isCorrect)
                    {
                        return BadRequest("invalid credentials");
                    }
                     var TokenMethode = new TokenMethode(_configuration,_refreshToken,_mediator);

                // generate token
                var jwt = await TokenMethode.GenerateToken(existingUser);
                      return Ok(new {Token = jwt});
            }
            return BadRequest("please you should register before");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Parameters");
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                _tokenValidationParameters.ValidateLifetime = false;

                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return BadRequest("Invalid Token");
                }

                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);
                var tokenMethode = new TokenMethode(_configuration, _refreshToken,_mediator);

                var expiryDate = tokenMethode.UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return BadRequest("Token has not yet expired");
                }

                var storedToken = await _mediator.Send(new RefreshTokenQuery {tokenHave = tokenRequest.Refreshtoken });

                if (storedToken == null || storedToken.IdRevoked || storedToken.IdUsed)
                {
                    return BadRequest("Invalid Token");
                }

                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;

                if (storedToken.JwtId != Guid.Parse(jti))
                {
                    return BadRequest("Invalid Token");
                }

                if (storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    return BadRequest("Refresh Token has expired");
                }

                storedToken.IdUsed = true;
                await _mediator.Send(new UpdateTokenCommand { refreshToken = storedToken });

                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());

                var newToken = await tokenMethode.GenerateToken(dbUser!);

                return Ok(newToken);
            }
            catch (Exception ex)
            {
                return BadRequest("Server error: " + ex.Message);
            }
        }


    }
}
