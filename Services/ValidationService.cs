using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using marketplaceE.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace marketplaceE.Services
{
    public interface IUserValidationService
    {
        Task<bool> UserRegistration(NewUserDto user);
      
    }
    public class ValidationService: IUserValidationService
    {
        private readonly AppDbContext _context;             

        public ValidationService(AppDbContext context)
        {
            _context = context;
                       
        }

        public async Task<bool> UserRegistration(NewUserDto dto)
        {
            var namePattern = @"^[a-zA-Zа-яА-ЯёЁ]+$";

            if(!Regex.IsMatch(dto.UserName, namePattern) || !(2<=dto.UserName.Length) || !( dto.UserName.Length<50))
            {
                throw new Exception("Имя должно содержать только буквы, быть длиннее 1-го символа и короче 50");
            }

            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]*$";
            if (! Regex.IsMatch(dto.PasswordHash, passwordPattern) || dto.PasswordHash.Length<10 || dto.PasswordHash.Length > 20)
            {
                throw new Exception("Пароль должен содержать минимум одну строчную, одну заглавную букву, одну цифру, быть длинной минимум 10 и максимум 20 символов,и может содержать спецсимволы.");
            }
            
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly minDate = new DateOnly(1900, 1, 1);
            if (dto.DateOfBirth >= today || dto.DateOfBirth< minDate)
            {
                throw new Exception($"Дата рождения должна быть где-то между 01.01.1900 и {today}");
            }
            var agee = today.Year - dto.DateOfBirth.Year;
            if (dto.DateOfBirth > today.AddYears(-agee))
            {
                agee--;
            }
            if(agee<18 && dto.Role == RolesOfUsers.Master)
            {
                throw new Exception("Мастер должен быть старше 18 лет");
            }
            if(agee<10 && dto.Role == RolesOfUsers.User) { throw new Exception("Для регистрации нужно быть старше 10 лет"); }

            return true;
        }

        
    }
}
