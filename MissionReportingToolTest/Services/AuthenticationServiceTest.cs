using MissionReportingTool.Configuration;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using MissionReportingToolTest.Mocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        private static readonly int TokenExpirationInMinutes = 5;
        private static readonly JwtTokenConfiguration JwtTokenConfiguration = new JwtTokenConfiguration() { Audience = "Audience", Issuer = "Issuer", SigningKey = "qwqeqwrasdasdasfweqedasdasfasfasasf", TokenExpirationInMinutes = TokenExpirationInMinutes };
        private static readonly IUserRepository UserRepository = new FakeUserRepository();
        private static readonly UserService UserService = new UserService(UserRepository);
        private static readonly IAuthenticationService AuthenticationService = new AuthenticationService(UserRepository, JwtTokenConfiguration);

        [TestCleanup]
        public async Task clean()
        {
            foreach (var x in await UserService.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await UserService.Delete(x.Id);
            }
        }

        private string getClaimFromJwtSecurityToken(JwtSecurityToken jwtSecurityToken, string claimName)
        {
            return jwtSecurityToken.Claims.First(claim => claim.Type == claimName).Value;
        }

        [TestMethod]
        public async Task testAuthenticate()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "Username", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var id = await UserService.Create(creationRequest);
            var token = await AuthenticationService.Authenticate(new AuthenticateRequest(creationRequest.Username, creationRequest.Password));
            Assert.IsTrue((token.validUntil - DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes)).TotalMinutes < 1);
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Token);
            Assert.IsNotNull(getClaimFromJwtSecurityToken(jwtSecurityToken, "jti"));
            Assert.AreEqual(id.ToString(), getClaimFromJwtSecurityToken(jwtSecurityToken, "userid"));
            Assert.AreEqual(creationRequest.Username, getClaimFromJwtSecurityToken(jwtSecurityToken, "username"));
            Assert.AreEqual(creationRequest.FirstName, getClaimFromJwtSecurityToken(jwtSecurityToken, "firstname"));
            Assert.AreEqual(creationRequest.LastName, getClaimFromJwtSecurityToken(jwtSecurityToken, "lastname"));
            Assert.AreEqual(creationRequest.CodeName, getClaimFromJwtSecurityToken(jwtSecurityToken, "codename"));
            Assert.AreEqual(creationRequest.Avatar, getClaimFromJwtSecurityToken(jwtSecurityToken, "avatar"));
            Assert.AreEqual(creationRequest.Role.ToString(), getClaimFromJwtSecurityToken(jwtSecurityToken, "role"));
        }

        [TestMethod]
        public async Task testAuthenticateWithNonExisitngUsernameThrows()
        {
            await Assert.ThrowsExceptionAsync<InvalidCredentialsException>(async () =>
            {
                await AuthenticationService.Authenticate(new AuthenticateRequest("NonExisitngUsername", "Password"));
            });
        }

        [TestMethod]
        public async Task testAuthenticateWithWrongPasswordThrows()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "Username", "Email", "Password", "Avatar", Role.ASTRONAUT);
            await UserService.Create(creationRequest);
            await Assert.ThrowsExceptionAsync<InvalidCredentialsException>(async () =>
            {
                await AuthenticationService.Authenticate(new AuthenticateRequest(creationRequest.Username, "NonExistingPassword"));
            });
        }
    }
}
