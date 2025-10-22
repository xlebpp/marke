using marketplaceE.appDbContext;
using marketplaceE.DTOs;
using marketplaceE.Models;
using marketplaceE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                return BadRequest(new List<ValidationError> { new ValidationError { Field = "Email", Message = "Пользователь с таким логином уже существует" } });
            }
            else
            {
                try
                {
                    var valid = await _validation.UserRegistration(dto);                                    
                    await _userService.AddNewUser(dto);
                    return Ok("Успешно!!");
                    
                }
                catch(ValidationException ex) 
                {
                    return BadRequest(ex.Errors);
                };

                
                
            }

        }
        [Route("api/authentic")]
        [HttpPost]
        public async Task <IActionResult> Autentication([FromBody] EnteranceUser user1)
        {

            Console.WriteLine("ВОТ БЛЯТЬ" + user1.Email + user1.PasswordHash); 
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
        [Authorize]
        [Route("api/user/avatar")]
        [HttpGet]
        public async Task <IActionResult> GetCircleUserAvatar()
        {
            var idd = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            if (idd == "0")
            {
                
                return Unauthorized();
            }

            var id = Convert.ToInt32(idd);
            var photo = await _userService.ShowCirclePhoto(id);
            return Ok(photo);
        }

        [Route("api/masters")]
        [HttpGet]
        public async Task<IActionResult> GetMasters()
        {
            var exsistance = await _userService.AreThereAnyMasters();
            if (!exsistance)
            {
                return NotFound("Ни одного мастера не найдено");
            }
            var masters = await _userService.ShowMasters();
            if (masters == null || masters.Count() == 0)
            {
                return BadRequest("Мастера не вернулись");
            }
            return Ok(masters);
        }

        [Route("api/master")] ///тут скорее для карточки, не понятно, нужен ли
        [HttpGet]
        public async Task<IActionResult> GetMasterProfile([FromQuery]int id)
        {
            var exsistance = await _userService.CheckUserExcistanceById(id);
            if (!exsistance)
            {
                return NotFound("Пользователь с таким id не найден");
            }

            var master = await _userService.OpenMasterProfile(id);

            if (master != null)
            {
                return Ok(master);
            }
            return BadRequest("Мастер не найден");
        }
        [Route("api/profile/user")]
        [HttpGet]
        public async Task<IActionResult> ShowProfileOfUser([FromQuery]int id)
        {
            var exsistance = await _userService.CheckUserExcistanceById(id);
            if (!exsistance) { return NotFound("Пользователь с таким id не найден"); }

            var role = await _userService.WhatRoleDoesTheUserHave(id);

            if (role == RolesOfUsers.Master)
            {
                var master = await _userService.ShowMasterProfile(id);
                if(master!= null) 
                {  
                    return Ok(master); 
                }
                else
                {
                    return BadRequest("Мастер пустой");
                }

            }
            var user = await _userService.ShowUserProfile(id);
            return Ok(user);


        }
    }
}
