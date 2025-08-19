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
                new Claim(type:"Email", userr.Email),
                new Claim(type:"Role", userr.Role),
                new Claim(type:"Id", id.ToString()),
                new Claim(type:"Name", userr.UserName)
            };
            var jwt = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials: new SigningCredentials
                                        (new SymmetricSecurityKey
                                            (Encoding.UTF8.GetBytes(options.Value.key)),
                                            algorithm:SecurityAlgorithms.HmacSha256)
                                        
                                         
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
