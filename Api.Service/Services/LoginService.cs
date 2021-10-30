using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos.Login;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private SigningConfigurations _signingConfigurations { get; set; }
        private TokenConfigurations _tokenConfigurations { get; set; }
        private IUserRepository _repository;

        public LoginService(IUserRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations
            )
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<object> FindByLoginAsync(LoginDto user)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
            {
                var baseUser = await _repository.FindByLoginAsync(user);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                         new GenericIdentity(user.Email),
                         new[]
                           {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                           }
                        );

                    DateTime createDate = DateTime.UtcNow;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, baseUser);
                }
            }
            else
            {
                return null;
            }
        }


        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                email = user.Email,
                name = user.UserName,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
