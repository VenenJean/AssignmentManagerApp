using Entities.Abstraction;
using Infrastructure.Persistence.Assignments.Commands.Modify;
using Infrastructure.Prompts;

namespace Infrastructure.Persistence.Assignments.Commands;

public class ModifyAssignmentCommand {
  private IAssignmentRepository assignmentRepository { get; }

  public ModifyAssignmentCommand(IAssignmentRepository assignmentRepository) {
    this.assignmentRepository = assignmentRepository;
  }

  public async Task HandleAsync() {
    Console.Write("[Position] > ");
    var position = Console.ReadLine();

    try {
      var assignment = await assignmentRepository.GetByPositionAsync(Convert.ToInt32(position));
      if (assignment is null) {
        Console.WriteLine("Assignment not found.");
        return;
      }

      Console.Write("[1. Name | 2. Swap positions | 3. Status | 4. Deadline] > ");
      var option = Console.ReadLine();

      if (option is null) {
        Console.WriteLine("Invalid option format.");
        return;
      }

      switch (option) {
        case "1": {
          new ModifyNameCommand(assignment).Handle();
          break;
        }
        case "2": {
          await new ModifyPositionCommand(assignment, assignmentRepository).HandleAsync();
          break;
        }
        case "3": {
          new ModifyStatusCommand(assignment).Handle();
          break;
        }
        case "4": {
          new ModifyDeadlineCommand(assignment).Handle();
          break;
        }
        default: {
          await assignmentRepository.SaveChangesAsync();
          break;
        }
      }
    }
    catch (FormatException) {
      Console.WriteLine("Invalid format. Required: Integer.");
    }
    catch (Exception ex) {
      Messages.PrintException(ex);
    }
  }
}