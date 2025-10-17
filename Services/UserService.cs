using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using Microsoft.EntityFrameworkCore;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace marketplaceE.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExsist(NewUserDto user);
        Task<bool> AddNewUser(NewUserDto user);
        Task<int> FindUser(EnteranceUser user);
        Task<string> CheckPassword(int id, EnteranceUser user);
        Task<JsonResult> ShowCirclePhoto(int id);
        Task<OpenMasterDto> OpenMasterProfile(int id);
        Task<bool> CheckUserExcistanceById(int id);
        Task<IEnumerable<ShowMasters>> ShowMasters();

        Task<bool> AreThereAnyMasters();
        Task<ShowMasterProfile> ShowMasterProfile(int id);
        Task<RolesOfUsers> WhatRoleDoesTheUserHave(int id);
        Task<ShowUserProfile> ShowUserProfile(int id);

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
            // var amount = await _context.Users.Where(u => u.Email == dto.Email).CountAsync();
            //if (amount > 1)
            // {
            //     return true;
            // }
            // var role = await _context.Users.Where(u => u.Email == dto.Email).Select(u => u.Role).FirstOrDefaultAsync();
            // if (dto.Role == role)
            //  {
            ///      return true;
            // }
            //  return false;
            var existance = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (existance) {
                return true;
            }
            return false;
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
            
            ///var passw = await _context.Users.Where(u => u.Id == id).Select(u=>u.PasswordHash).FirstOrDefaultAsync();
            var verifyResult = _hasher.VerifyHashedPassword(userr, userr.PasswordHash, user1.PasswordHash);
            
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Пароль не подходит");
            }

            var user = await _context.Users.Where(u => u.Id == id)
                    .Select(u => new TokenUSerDto
                    {
                        Role = u.Role,
                        Email = u.Email,
                        UserName = u.UserName

                    }).FirstOrDefaultAsync();
            
            if (user == null) { throw new Exception("Пользователь не найден"); }
            return _jwtService.GenerateTocken(user, id);

        }

        public async Task<JsonResult> ShowCirclePhoto(int id)
        {
            var photo = await _context.Users.Where(u => u.Id == id).Select(u => u.UserPhoto).FirstOrDefaultAsync();
            
            return new JsonResult(new { photo = photo!=null ? Convert.ToBase64String(photo) : null });
        }
        public async Task<bool> CheckUserExcistanceById(int id)
        {
            var excistance = await _context.Users.AnyAsync(u => u.Id == id);
            if(excistance) { return true; }
            return false;
        }

        public async Task<OpenMasterDto> OpenMasterProfile(int id)///нужен ли мне этот метод????
        {
            var masterr = await _context.Users
                           .Where(u => u.Id == id)
                           .FirstOrDefaultAsync();
            var products = await _context.Products
                .Where(u => u.MasterId == id)
                .Include(o => o.Orders).ThenInclude(r => r.Review).ToListAsync();
            double rating=0;
            var ratings=products
                .SelectMany(p=>p.Orders)
                .Where(o => o.Review != null)
                .Select(o => o.Review.Rating)
                .ToList();

            if (ratings.Any())
            {
                rating = ratings.Average();
            }              
                                   
            var master = new OpenMasterDto
            {
                MasterId=id,
                Name = masterr.UserName,
                UserPhoto=masterr.UserPhoto!=null ? Convert.ToBase64String(masterr.UserPhoto) : null,
                Products=masterr.Products!=null ? masterr.Products.ToList() : null,
                Rating=rating
            };
            return master;
        }

        public async Task<bool> AreThereAnyMasters()
        {
            var exsistance = await _context.Users.AnyAsync(u => u.Role == RolesOfUsers.Master);
            if (!exsistance)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ShowMasters>> ShowMasters()
        {
            var masters = await _context.Users.Where(u => u.Role == RolesOfUsers.Master).ToListAsync();

            var result = masters.Select(u => new ShowMasters
            {
                MasterId = u.Id,
                Name = u.UserName,
                UserPhoto = u.UserPhoto != null ? Convert.ToBase64String(u.UserPhoto) : null,
                About = u.About
            });

            return result;
        }

        public async Task<RolesOfUsers> WhatRoleDoesTheUserHave(int id)
        {
            var role = await _context.Users.Where(i => i.Id == id).Select(i => i.Role).FirstOrDefaultAsync();
            return role;
        }

        public async Task<ShowMasterProfile> ShowMasterProfile(int id)
        {
            var masterr = await _context.Users.Where(i => i.Id == id).FirstOrDefaultAsync();

            var products = await _context.Products.Where(m=>m.MasterId==id).Include(o=>o.Orders).ThenInclude(r=>r.Review).ToListAsync();

            double rating = 0;
            var ratings = products.SelectMany(o=>o.Orders).Where(r=>r.Review!=null).Select(r=>r.Review.Rating).ToList();
            if (ratings.Any())
            {
                rating = ratings.Average();
            }

            var master = new ShowMasterProfile
            {
                Id=masterr.Id,
                Name = masterr.UserName,
                About = masterr.About!=null ? masterr.About: "Пока ничего",
                Rating = rating,
                Products = masterr.Products!=null? masterr.Products.ToList() : null,
                UserPhoto = masterr.UserPhoto!=null ? Convert.ToBase64String(masterr.UserPhoto) : null
            };

            return master;
        }

        public async Task<ShowUserProfile> ShowUserProfile(int id)
        {
            var usert = await _context.Users.Where(i => i.Id == id).FirstOrDefaultAsync();

            var user = new ShowUserProfile
            {
                Id = usert.Id,
                Name = usert.UserName,
                About = usert.About != null ? usert.About : "Пока ничего",
                UserPhoto = usert.UserPhoto != null ? Convert.ToBase64String(usert.UserPhoto) : null,
                Requests = usert.Requests != null ? usert.Requests.ToList() : null

            };

            return user;
        }
    }
}

