using Core.Entities;
using FluentResults;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<Result> AddAsync(User usuario);
    Task<Result<User>> GetByEmailAsync(string email);
    Task<Result<User>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<User>>> GetAllAsync();
    Task<Result> UpdateAsync(User usuario);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<bool>> ExistsAsync(Guid id);
}