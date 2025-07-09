using Core.Entities;
using Core.Errors;
using Core.Interfaces;
using FluentResults;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> AddAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result<User>> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Users
            .Include(u => u.TimeRecords)
            .FirstOrDefaultAsync(u => u.Email == email);

        return user is null
            ? Result.Fail(new NotFoundError("No user found with the provided email."))
            : Result.Ok(user);
    }

    public async Task<Result<User>> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Users
            .Include(u => u.TimeRecords)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user is null
            ? Result.Fail(new NotFoundError("User not found."))
            : Result.Ok(user);
    }

    public async Task<Result<IEnumerable<User>>> GetAllAsync()
    {
        var users = await _dbContext.Users
            .Include(u => u.TimeRecords)
            .ToListAsync();

        return Result.Ok(users.AsEnumerable());
    }

    public async Task<Result> UpdateAsync(User user)
    {
        var existingUser = await _dbContext.Users.FindAsync(user.Id);
        if (existingUser is null)
            return Result.Fail(new NotFoundError("User not found for update."));

        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.PasswordHash = user.PasswordHash;
        existingUser.Role = user.Role;

        _dbContext.Users.Update(existingUser);
        await _dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null)
            return Result.Fail(new NotFoundError("User not found for deletion."));

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result<bool>> ExistsAsync(Guid id)
    {
        var exists = await _dbContext.Users.AnyAsync(u => u.Id == id);
        return Result.Ok(exists);
    }
}
