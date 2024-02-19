using Entities.Models;

namespace Infrastructure.Persistence.Assignments.Commands.Modify;

public class ModifyNameCommand {
  private Assignment assignment { get; }
  
  public ModifyNameCommand(Assignment assignment) {
    this.assignment = assignment;
  }

  public void Handle() {
    Console.Write("[New name] > ");
          
    // Getting and evaluating new name
    var newName = Console.ReadLine();
    if (newName is null or "") {
      Console.WriteLine("Invalid name format.");
      return;
    }

    assignment.Name = newName;
    Console.WriteLine("New name set!");
  }
}