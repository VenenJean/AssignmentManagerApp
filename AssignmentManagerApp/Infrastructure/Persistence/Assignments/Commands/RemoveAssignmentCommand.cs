using Entities.Abstraction;

namespace Infrastructure.Persistence.Assignments.Commands;

public class RemoveAssignmentCommand {
  private IAssignmentRepository assignmentRepository { get; }
  
  public RemoveAssignmentCommand(IAssignmentRepository assignmentRepository) {
    this.assignmentRepository = assignmentRepository;
  }
  
  public async Task HandleAsync() {
    Console.Write("[Position] > ");
    var position = Console.ReadLine();

    try {
      var assignmentToDelete = await assignmentRepository.GetByPositionAsync(Convert.ToInt32(position));
      if (assignmentToDelete is null) {
        Console.WriteLine("Assignment not found.");
        return;
      }

      await assignmentRepository.DeleteAsync(assignmentToDelete.Id);
      await assignmentRepository.SaveChangesAsync();
      Console.WriteLine("Assignment removed successfully.");
    }
    catch (FormatException) {
      Console.WriteLine("Invalid format. Required: Integer.");
    }
  }
}