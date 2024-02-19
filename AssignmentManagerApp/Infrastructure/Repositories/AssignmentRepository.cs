using Entities.Abstraction;
using Entities.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository {
  public AssignmentRepository(AppDbContext context) : base(context) { }

  public async Task<Assignment?> GetByPositionAsync(int position) {
    return await DbSet.FirstOrDefaultAsync(assignment => assignment.Position == position);
  }

  public async Task<Assignment?> GetByNameAsync(string name) {
    return await DbSet.FirstOrDefaultAsync(assignment => assignment.Name == name);
  }
}