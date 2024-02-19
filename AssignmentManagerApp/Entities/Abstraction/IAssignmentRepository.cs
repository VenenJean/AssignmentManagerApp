using Entities.Models;

namespace Entities.Abstraction;

public interface IAssignmentRepository : IGenericRepository<Assignment> {
  Task<Assignment?> GetByPositionAsync(int position);
  Task<Assignment?> GetByNameAsync(string name);
}