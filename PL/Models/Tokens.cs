using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace PL.Models
{
    public class Tokens
    {
        public static string GenerateJwtToken(ML.Usuario user)
        {
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyId = ConfigurationManager.AppSettings["JwtKey"]; // Asigna un valor adecuado para el Key ID
            var securityKey = new SymmetricSecurityKey(key) { KeyId = keyId };
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static bool ValidateToken(string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtKey"]);
                var keyId = ConfigurationManager.AppSettings["JwtKey"]; // Asigna un valor adecuado para el Key ID
                var securityKey = new SymmetricSecurityKey(key) { KeyId = keyId };

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    ValidateIssuer = false, // Puedes habilitar si tu token tiene información del emisor (issuer)
                    ValidateAudience = false, // Puedes habilitar si tu token tiene información del público objetivo (audience)
                    ValidateLifetime = true, // Validar la vigencia del token

                    ClockSkew = TimeSpan.Zero // Puedes ajustar el tiempo de tolerancia si es necesario
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var nombreClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                return true;
            }
            catch (Exception ex)
            {
                // La excepción se lanza si el token no es válido
                Console.WriteLine($"Error al validar el token: {ex.Message}");
                return false;
            }
        }
    }
}