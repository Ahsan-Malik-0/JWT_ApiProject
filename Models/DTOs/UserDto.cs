using System;

namespace WebProjectAPIs.Models.DTOs;

public class CreateUserDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}

public class RegisterUserDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}

public class LoginUserDto
{
    public string? UserName { get; set; }
    public required string Password { get; set; }
}
