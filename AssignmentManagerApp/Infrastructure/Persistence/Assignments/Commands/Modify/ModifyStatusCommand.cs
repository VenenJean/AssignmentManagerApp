using Entities.Models;

namespace Infrastructure.Persistence.Assignments.Commands.Modify;

public class ModifyStatusCommand(Assignment assignment) {
  private Assignment assignment { get; } = assignment;

  public void Handle() {
    // Inverting current status
    assignment.Status = !assignment.Status;
    Console.WriteLine("New status set!");
  }
}