using IK.SCP.Application.SEG.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServiceReference;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IK.SCP.Application.Common.Helpers
{
    public class AuthHelper
    {
        //public string GenerateToken(LoginRequest request, string secret)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name, request.Usuario),
        //            new Claim(ClaimTypes.Role, request.Rol.ToString()),
        //            new Claim(ClaimTypes.Email, request.Correo),
        //            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new{ 
        //                request.Usuario,
        //                request.Nombre,
        //                request.Modulo,
        //                request.Linea,
        //                request.LineaDesc,
        //                request.Rol,
        //                request.RolDesc,
        //                request.Articulo,
        //                request.Cantidad,
        //                request.Fecha,
        //                request.Orden
        //            }))
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(3),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public string GenerateToken(Credencial credencial, Rol rol, List<Nodo> nodos, object? acciones, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, credencial.UsuarioAlias),
                    new Claim(ClaimTypes.Role, rol.RolId.ToString()),
                    new Claim(ClaimTypes.Email, credencial.Correo ?? ""),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new{
                        Usuario = credencial.UsuarioAlias,
                        Nombre = credencial.NombrePersonal,
                        Rol = rol.RolId,
                        RolDesc = rol.Nombre,
                        Opciones = nodos.OrderBy(o => o.Orden).ToList(),
                        Acciones = acciones,
                    }))
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

}
