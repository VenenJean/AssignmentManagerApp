using Entities.Abstraction;
using Entities.Models;

namespace Infrastructure.Persistence.Assignments.Commands;

public class AddAssignmentCommand {
  private User user { get; }
  private IAssignmentRepository assignmentRepository { get; }
  
  public AddAssignmentCommand(IAssignmentRepository assignmentRepository, User user) {
    this.user = user;
    this.assignmentRepository = assignmentRepository;
  }
  
  public async Task HandleAsync() {
    Console.Write("[Assignment name] > ");
    var name = Console.ReadLine();
    
    if (name is null or "") {
      Console.WriteLine("Invalid name format.");
      return;
    }
    
    Console.Write("[Assignment deadline year] > ");
    var year = Console.ReadLine();

    Console.Write("[Assignment deadline month] > ");
    var month = Console.ReadLine();
    
    Console.Write("[Assignment deadline day] > ");
    var day = Console.ReadLine();


    DateOnly deadline;
    try {
      deadline = new DateOnly(int.Parse(year!), int.Parse(month!), int.Parse(day!));
    }
    catch (ArgumentOutOfRangeException) {
      Console.WriteLine("Date is out of range.");
      return;
    }
    catch (FormatException) {
      Console.WriteLine("Invalid data format. Assignment not added.");
      return;
    }

    if (await assignmentRepository.GetByNameAsync(name) is not null) {
      Console.WriteLine($"Assignment '{ name }' already exists.");
      return;
    } 
    var assignment = new Assignment(Guid.NewGuid(), 4, false, name, deadline);
    assignment.User = user;
    
    await assignmentRepository.InsertAsync(assignment);
    await assignmentRepository.SaveChangesAsync();
    Console.WriteLine("Assignment added successfully.");
  }
}