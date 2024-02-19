using Entities.Abstraction;
using Entities.Models;
using Infrastructure.Persistence.Assignments.Commands;

namespace Infrastructure.Prompts;

public class CommandLine {
  private User dbUser { get; }
  private IAssignmentRepository assignmentRepository { get; }
  private AssignmentsUi assignmentsUi { get; }
  
  public CommandLine(User dbUser, IAssignmentRepository assignmentRepository, AssignmentsUi assignmentsUi) {
    this.dbUser = dbUser;
    this.assignmentRepository = assignmentRepository;
    this.assignmentsUi = assignmentsUi;
  }
  
  public async Task HandleAsync() { 
    PrintMainScreen();
    
    while (true) {
      Console.Write("\n[1. Add | 2. Remove | 3. Modify | 4. Show assignments | 5. Clear | 6. Exit] > ");
      var input = Console.ReadKey().KeyChar;
      Console.WriteLine();

      switch (input) {
        case '1':
        case 'a': {
          await new AddAssignmentCommand(assignmentRepository, dbUser).HandleAsync();
          continue;
        }
        case '2': 
        case 'r': {
          await new RemoveAssignmentCommand(assignmentRepository).HandleAsync();
          continue;
        }
        case '3': 
        case 'm': {
          await new ModifyAssignmentCommand(assignmentRepository).HandleAsync();
          continue;
        }
        case '4': 
        case 's': {
          assignmentsUi.PrintAssignments();
          continue;
        }
        case '5': 
        case 'c': {
          Console.Clear();
          PrintMainScreen();
          continue;
        }
        case '6': 
        case 'e': 
        case 'q': {
          Console.WriteLine("Logged out.");
          break;
        }
        default: {
          Console.WriteLine("\nInvalid operation.");
          continue;
        }
      }
      
      break;
    }
  }

  private void PrintMainScreen() {
    Console.WriteLine($"\n\n ### { ToPossessiveForm(dbUser.Name) } tasks ###\n");
    assignmentsUi.PrintAssignments();
  }
  
  private string ToPossessiveForm(string name) {
    return name.Insert(name.Length, name.EndsWith('s') ? "'" : "'s");
  }
}