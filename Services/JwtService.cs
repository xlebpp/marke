using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using marketplaceE.JwtSettings;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;


namespace marketplaceE.Services
{

    public class JwtService(IOptions<JwtSetting> options)
    {
       
        public string GenerateTocken(TokenUSerDto userr, int id)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userr.Email),
                new Claim(ClaimTypes.Role, userr.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, userr.UserName)
            };
            var jwt = new JwtSecurityToken(
                //issuer: "marketplaceE",
                ///audience: "marketplaceEUsers",
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials: new SigningCredentials
                                        (new SymmetricSecurityKey
                                            (Encoding.UTF8.GetBytes(options.Value.Key)),
                                            algorithm:SecurityAlgorithms.HmacSha256)
                                        
                                         
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
