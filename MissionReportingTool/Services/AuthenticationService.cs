using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MissionReportingTool.Configuration;
using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Entities;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MissionReportingTool.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService UserService;
        private readonly JwtTokenConfiguration JwtTokenConfiguration;

        public AuthenticationService(IUserService userService, JwtTokenConfiguration jwtTokenConfiguration)
        {
            this.UserService = userService;
            this.JwtTokenConfiguration = jwtTokenConfiguration;
        }

        public async Task<JwtTokenResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await UserService.GetByUsernameAndPassword(request);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("firstname", user.FirstName),
                new Claim("lastname", user.LastName),
                new Claim("codename", user.CodeName),
                new Claim("avatar", user.Avatar),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenConfiguration.SigningKey));
            var credetials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: JwtTokenConfiguration.Issuer,
                audience: JwtTokenConfiguration.Audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtTokenConfiguration.TokenExpirationInMinutes)),
                claims: claims,
                signingCredentials: credetials
            );

            return new JwtTokenResponse(new JwtSecurityTokenHandler().WriteToken(jwtToken), jwtToken.ValidTo);
        }
    }
}
