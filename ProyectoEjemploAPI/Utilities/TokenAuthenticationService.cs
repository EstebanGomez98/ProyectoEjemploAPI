using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProyectoEjemploAPI.Context;
using ProyectoEjemploAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEjemploAPI.Utilities
{
    public class TokenAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly TokenManagement _tokenManagement;
        private readonly AppDbContext context;
        public TokenAuthenticationService(IUserService userService, IOptions<TokenManagement> tokenManagement, AppDbContext context)
        {
            _userService = userService;
            _tokenManagement = tokenManagement.Value;
            this.context = context;
        }
        public bool IsAuthenticated(LoginRequestModel request, out string token)
        {
            token = string.Empty;
            if (!_userService.IsValid(request))
                return false;

            var user = context.USUARIO.FirstOrDefault(f => f.Email == request.Email);
            if (user != null)
            {
                if (user.Pass == request.Pass)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email,request.Email),
                        new Claim(ClaimTypes.Role,user.Id_rol+"")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.IssuerSigningKey));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwtToken = new JwtSecurityToken(
                        _tokenManagement.ValidIssuer,
                        _tokenManagement.ValidAudience,
                        claims,
                        expires: DateTime.Now.AddMinutes(_tokenManagement.ExpirationTimeMinutes),
                        signingCredentials: credentials);

                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }

        }
    }
}
