using Entities.Abstraction;
using Entities.Models;

namespace Infrastructure.Persistence.Assignments.Commands.Modify;

public class ModifyPositionCommand {
  private Assignment assignment { get; }
  private IAssignmentRepository assignmentRepository { get; }
  
  public ModifyPositionCommand(Assignment assignment, IAssignmentRepository assignmentRepository) {
    this.assignment = assignment;
    this.assignmentRepository = assignmentRepository;
  }
  
  public async Task HandleAsync() {
    Console.Write("[New position] > ");
    var newPosition = Console.ReadLine();
    // Checking for invalid input
    if (newPosition is null) {
      Console.WriteLine("Invalid position format.");
      return;
    }

    // Swapping with existing item on that position
    var existingAssignment = await assignmentRepository.GetByPositionAsync(Convert.ToInt32(newPosition));
    if (existingAssignment is not null) {
      existingAssignment.Position = assignment.Position;
    }
    else {
      Console.WriteLine("Assignment not found.");
      return;
    }
    assignment.Position = Convert.ToInt32(newPosition);
          
    Console.WriteLine("New position set!");
  }
}