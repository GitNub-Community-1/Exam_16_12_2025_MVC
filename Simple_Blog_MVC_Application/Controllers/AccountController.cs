using Domain.Dtos;
using Domain.Models;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Simple_Blog_MVC_Application.Controllers;

public class AccountController(IAccountService service, SignInManager<User> signInManager) : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        return View(new LoginDto() { ReturnUrl = returnUrl });
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var response = await service.RegisterAsync(model);
        if (response.StatusCode == 200)
        {
            return RedirectToAction("Login", "Account");
        }
        
        ModelState.AddModelError(string.Empty,string.Join("\n",response.Errors));
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid == false)
            return View(model);
 
        var response = await service.LoginAsync(model);
        if (response.StatusCode == 200)
        { 
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty,string.Join("\n",response.Errors));
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}