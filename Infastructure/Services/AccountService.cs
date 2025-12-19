using System.Net;
using System.Security.Claims;
using Domain.Dtos;
using Domain.Models;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infastructure.Services;

public class AccountService(UserManager<User> userManager, SignInManager<User> signInManager) : IAccountService
{
     public async Task<Response<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username);
            if (user is not null)
            {
                var isValidPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (isValidPassword)
                {
                    var claims = new List<Claim>()
                    {
                        new (ClaimTypes.Name, user.UserName!),
                        new (ClaimTypes.Email, user.Email!),
                        new ("FirstName", user.FirstName)
                    };
                    
                    await signInManager.SignInWithClaimsAsync(user, true, claims);
                    return new Response<LoginResponseDto>(new LoginResponseDto()
                    {
                        User =user,
                        Claims = claims
                    });
                }
                else
                {
                    return new Response<LoginResponseDto>(HttpStatusCode.BadRequest, "password is incorrect");
                }
            }
            return new Response<LoginResponseDto>(HttpStatusCode.BadRequest, "login or password is incorrect");
        }
        public async Task<Response<RegisterDto>> RegisterAsync(RegisterDto model)
        {
            var mapped = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var response = await userManager.CreateAsync(mapped,model.Password);
            if (response.Succeeded)
                return new Response<RegisterDto>(model);
            else return new Response<RegisterDto>(HttpStatusCode.BadRequest, response.Errors.Select(e=>e.Description).ToList());

        }
}