using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using Microsoft.EntityFrameworkCore;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;


namespace marketplaceE.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExsist(NewUserDto user);
        Task<bool> AddNewUser(NewUserDto user);
        Task<int> FindUser(EnteranceUser user);
        Task<string> CheckPassword(int id, EnteranceUser user);
      

    }
    public class UserService:IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JwtService _jwtService;

        public UserService(AppDbContext context, IPasswordHasher<User> hasher, JwtService jwtService)
        {
            _context = context;
            _hasher = hasher;
            _jwtService = jwtService;
        }

        public async Task<bool> IsUserExsist(NewUserDto dto)
        {
            var exsistance = await _context.Users.AnyAsync(c => c.Email == dto.Email);
            if (exsistance)
            {
                return true;

            }
            else { return false; }
        }
        
        public async Task<bool> AddNewUser(NewUserDto dto)
        {

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Role = dto.Role,
                DateOfBirth = dto.DateOfBirth,
                CreatedAt = DateTime.UtcNow

            };

            user.PasswordHash = _hasher.HashPassword(user, dto.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> FindUser(EnteranceUser user1)
        {
            var exsistance = await _context.Users.AnyAsync(u => u.Email == user1.Email);
            if (exsistance)
            {
                var id = await _context.Users.Where(u => u.Email == user1.Email).Select(u => u.Id).FirstOrDefaultAsync();
                return id;

                
            }
            else
            {
                return -1;
            }                
           
        }

        

        public async Task<string> CheckPassword(int id, EnteranceUser user1)
        {
            var userr = await _context.Users.FirstOrDefaultAsync(u => u.Email == user1.Email);
            var passw = await _context.Users.Where(u => u.Id == id).Select(u=>u.PasswordHash).FirstOrDefaultAsync();
            var verifyResult = _hasher.VerifyHashedPassword(userr, userr.PasswordHash, user1.PasswordHash);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Пароль не подходит");
            }
            
               var user = await _context.Users.Where(u=>u.Id==id)
                    .Select(u=> new TokenUSerDto
                    {
                        Role=u.Role.ToString(),
                        Email = u.Email,
                        UserName=u.UserName

                    }).FirstOrDefaultAsync();
                if (user == null) { throw new Exception("Пользователь не найден"); }
                return _jwtService.GenerateTocken(user, id);
            
        }
    }
}

