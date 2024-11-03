using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WsVenta.Models;
using WsVenta.Models.Common;
using WsVenta.Models.Request;
using WsVenta.Models.Response;
using WsVenta.Tools;

namespace WsVenta.Services
{
    public class UserService : IUserService
    {
        private readonly AppSenttings _appSenttings;

        public UserService(IOptions<AppSenttings> appSenttings) 
        {

            _appSenttings = appSenttings.Value;

        }
        public UserResponse Auth(AuthRequest model)
        {   
            UserResponse userResponse = new UserResponse();
            using (var db = new VentaContext())
            {
                string spassword = Encrypt.GetSHA256(model.Password);

                var usuario = db.Usuario.Where(d => d.Email == model.Email &&
                 
                                               d.Password == spassword).FirstOrDefault();
                if (usuario == null) return null;
                
                userResponse.Email = usuario.Email;
                userResponse.Token = GetToken(usuario);
            }
            return userResponse;
             
        }
        private string GetToken(Usuario usuario) 
        {

            var tokenHander = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSenttings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),

                        new Claim(ClaimTypes.Email,usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHander.CreateToken(tokenDescriptor);
            return tokenHander.WriteToken(token);
        }
    }
}
