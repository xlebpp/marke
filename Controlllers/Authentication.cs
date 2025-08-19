using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using marketplaceE.Models;
using marketplaceE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marketplaceE.Controlllers
{
    [ApiController]
    
    //[Route""] - как мы приходим к этому месту? приходит запрос на вход, отдельно на регистрацию
    public class AuthControllers: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidationService _validation;

        public AuthControllers(IUserService userService, IUserValidationService validation)
        {
            _userService = userService;
            _validation = validation;
        }

        [Route("api/registr")]
        [HttpPost]
        public async Task <IActionResult> AddNewUser([FromBody] NewUserDto dto)
        {
           
            bool exsistance = await _userService.IsUserExsist(dto);
            if (exsistance)
            {
                return BadRequest("Пользователь с таким логином уже существует");
            }
            else
            {
                var valid = await  _validation.UserRegistration(dto);
                if(valid) 
                {
                    await _userService.AddNewUser(dto);
                    return Ok("Успешно!!");
                }
                else { return BadRequest("Ошибка валидации"); }
                
            }

        }
        [Route("api/authentic")]
        [HttpGet]
        public async Task <IActionResult> Autentication([FromQuery] EnteranceUser user1)
        {
            int id = await _userService.FindUser(user1);

            if (id < 0)
            {
                return BadRequest($"Ошибка! Пользователь с id = {id} не найден");
            }
            else
            {
                
                var tocken = await _userService.CheckPassword(id, user1);

                if(tocken is not null)
                {
                    return Ok(tocken);
                }
                else { return BadRequest(tocken); }
            }

        }

    }
}
