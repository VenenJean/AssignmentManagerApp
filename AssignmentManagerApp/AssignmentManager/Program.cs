using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Prompts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

// Initialise objects
var dbContext = new AppDbContext();
await dbContext.Database.MigrateAsync();
var assignmentRepository = new AssignmentRepository(dbContext);
var userRepository = new UserRepository(dbContext);

// Process...
await new Setup().SetupAppAsync();
var credentials = await new Authentication(userRepository).HandleAsync();

if (credentials is null) {
  Console.WriteLine("Invalid Username & Password combination.");
  return;
}

var username = credentials.Value.Item1;
var user = await userRepository.GetByUsernameAsync(username);
if (user is null) {
  Console.WriteLine($"The user {username} was not found.");
  return;
}

var assignmentsUi = new AssignmentsUi(user.Assignments);
var commandLine = new CommandLine(user, assignmentRepository, assignmentsUi);
await commandLine.HandleAsync();

/* ## APPLICATION DESIGN ##
 * 
 * ## <Name>('s) tasks ##
 * 1. [ ] Item1 | DateTime => In progress...
 * 2. [-] Item2 | DateTime => Missed!
 * 3. [X] Item3 | DateTime => Completed afterwards.
 * 3. [X] Item3 | DateTime => Completed.
 * 
 * OR => All tasks completed! Good work!
*/

/* CLI
 * [1. Add | 2. Remove{int} | 3. Modify{int}] >
 * 1. => Prompt for Task and deadline.
 * => Check deadline to be in the future.
 *
 * 2. => If completed: silent delete
 * else: ask for confirmation
 *
 * 3. => [1. Status | 2. Name{string} | 3. Deadline{DateTime}] >
 * 1.' => swap status
 * 2.' => prompt for new name | same as old forbidden
 * 3.' => prompt for deadline | check deadline to be in the future
 */
 
 /* Authentication
  * Prompt for User / Password
  * Check db:
  * match: load according assingments
  * not match: deny access
  */