using Microsoft.EntityFrameworkCore;
using WebProjectAPIs.Database;
using WebProjectAPIs.Models;
using WebProjectAPIs.Models.DTOs;

namespace WebProjectAPIs.Services;

public class UserServices(DB _db)
{
    public async Task<List<User>> GetAllUsers()
    {
        var users = await _db.Users.ToListAsync();
        return users;
    }
    
    public async Task<User> GetUserById(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new Exception("User not found");
        
        return user;
    }

    public async Task AddUser(CreateUserDto userDto)
    {
        if (userDto == null)
            throw new ArgumentNullException(nameof(userDto), "User cannot be null");
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            Role = userDto.Role
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, CreateUserDto userDto)
    {
        if (userDto == null)
            throw new ArgumentNullException(nameof(userDto), "User cannot be null");

        var existingUser = await _db.Users.FindAsync(id);
        if (existingUser == null)
            throw new Exception("User not found");

        existingUser.Name = userDto.Name;
        existingUser.Email = userDto.Email;
        existingUser.Password = userDto.Password;
        existingUser.Role = userDto.Role;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new Exception("User not found");

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}
