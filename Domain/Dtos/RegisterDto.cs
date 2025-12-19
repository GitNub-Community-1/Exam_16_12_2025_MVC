using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName{get;set;}
     [Required]
    public string Username{get;set;}
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
    [Compare("Password")]
    [Required]
    public string ConfirmPassword { get; set; }
    
}