using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFincance.API.DTOs;
using WebFincance.API.Models;
using WebFincance.API.Data;

namespace WebFinance.API.Services;


public class UserService : IUserService
{
    private readonly FinanceContext _context;

    public UserService(FinanceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _context.Users
                                  .Select(u => new UserDTO
                                  {
                                      Id = u.Id,
                                      Name = u.Name,
                                      Email = u.Email
                                  })
                                  .ToListAsync();
        return users;
    }

    public async Task<UserDTO> GetByIdAsync(int id)
    {
        var user = await _context.Users
                                 .Where(u => u.Id == id)
                                 .Select(u => new UserDTO
                                 {
                                     Id = u.Id,
                                     Name = u.Name,
                                     Email = u.Email
                                 })
                                 .FirstOrDefaultAsync();
        return user;
    }

    public async Task<UserDTO> CreateAsync(UserDTO userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email
            // Assign other fields as necessary
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        userDto.Id = user.Id;
        return userDto;
    }

    public async Task<bool> UpdateAsync(UserDTO userDto)
    {
        var user = await _context.Users.FindAsync(userDto.Id);
        if (user == null)
        {
            return false;
        }

        user.Name = userDto.Name;
        user.Email = userDto.Email;
        // Update other fields as necessary

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
