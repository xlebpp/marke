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
            var errors = new List<ValidationError>();

            var namePattern = @"^[a-zA-Zа-яА-ЯёЁ]+$";

            if(!Regex.IsMatch(dto.UserName, namePattern) || !(2<=dto.UserName.Length) || !( dto.UserName.Length<50))
            {
               errors.Add(new ValidationError {Field= "UserName", Message= "Имя должно содержать только буквы, быть длиннее 1-го символа и короче 50" });
            }

            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]*$";
            if (! Regex.IsMatch(dto.PasswordHash, passwordPattern) || dto.PasswordHash.Length<10 || dto.PasswordHash.Length > 20)
            {
              errors.Add(new ValidationError {Field= "PasswordHash", Message="Пароль должен содержать минимум одну строчную,\n одну заглавную букву, одну цифру,\n быть длинной минимум 10 и максимум 20 символов,\nи может содержать спецсимволы." });
            }
            
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly minDate = new DateOnly(1900, 1, 1);
            if (dto.DateOfBirth >= today || dto.DateOfBirth< minDate)
            {
               errors.Add(new ValidationError {Field= "DateOfBirth", Message=$"Дата рождения должна быть где-то между 01.01.1900 и {today}"});
            }
            var agee = today.Year - dto.DateOfBirth.Year;
            if (dto.DateOfBirth > today.AddYears(-agee))
            {
                agee--;
            }
            if(agee<18 && dto.Role == RolesOfUsers.Master)
            {
                errors.Add(new ValidationError {Field="Role",Message= "Мастер должен быть старше 18 лет"});
            }
            if(agee<10) { errors.Add(new ValidationError {Field = "DateOfBirth",Message ="Для регистрации нужно быть старше 10 лет"}); }

            if (errors.Any())
            {
                throw new ValidationException("Ошибка валидации", errors);
            }

            return true;
        }

        
    }
}
