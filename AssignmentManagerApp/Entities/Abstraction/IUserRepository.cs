using Entities.Models;

namespace Entities.Abstraction;

public interface IUserRepository : IGenericRepository<User> {
  Task<User?> GetByUsernameAsync(string name);
}