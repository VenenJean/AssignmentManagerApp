using Entities.Abstraction;
using Entities.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository {
  public async Task<User?> GetByUsernameAsync(string name) {
    return await DbSet
      .Include(user => user.Assignments)
      .FirstOrDefaultAsync(t => t.Name == name);
  }
}