using Entities.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Setup {
  public async Task SetupAppAsync() {
    var user = GenerateUser();
    await DbToDefault(user);
  }

  private User GenerateUser() {
    var localUser = new User(Guid.NewGuid(), "VenenJean", "Password123#");
    var future = new DateOnly(2025, 1, 1);
    var past = new DateOnly(2023, 1, 1);
    
    var assignments = new List<Assignment> {
      new(Guid.NewGuid(), 0, true, "Learn Clean Architecture", future),
      new(Guid.NewGuid(), 1, true, "Learn Repository Pattern", past),
      new(Guid.NewGuid(), 2, false, "Learn UnitOfWork Pattern", future),
      new(Guid.NewGuid(), 3, false, "Learn CQRS Pattern", past),
    };
    localUser.Assignments.AddRange(assignments);

    return localUser;
  }

  private async Task DbToDefault(User userObj) {
    var dbContext = new AppDbContext();

    await dbContext.Users.ForEachAsync(user => dbContext.Users.Remove(user));
    await dbContext.Assignments.ForEachAsync(assignment => dbContext.Assignments.Remove(assignment));

    await dbContext.Users.AddAsync(userObj);

    await dbContext.SaveChangesAsync();
  }
}