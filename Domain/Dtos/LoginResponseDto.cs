using System.Security.Claims;
using Domain.Models;

namespace Domain.Dtos;

public class LoginResponseDto
{
    public User User {get;set;}
    public List<Claim> Claims { get; set; }
}