using Entities.Models;

namespace Infrastructure.Persistence.Assignments.Commands.Modify;

public class ModifyDeadlineCommand {
  private Assignment assignment { get; }
  
  public ModifyDeadlineCommand(Assignment assignment) {
    this.assignment = assignment;
  }
  
  public void Handle() {
    // Getting deadline year and evaluating
    Console.Write("[Assignment deadline year] > ");
    var year = Console.ReadLine();
    if (year is null || year.Length != 4) {
      Console.WriteLine("Invalid year format. Consider a 4 digital number.");
      return;
    }

    // Getting deadline month and evaluating
    Console.Write("[Assignment deadline month] > ");
    var month = Console.ReadLine();
    if (month is null || month.Length > 2) {
      Console.WriteLine("Invalid month format.");
      return;
    }
    
    // Getting deadline day and evaluating
    Console.Write("[Assignment deadline day] > ");
    var day = Console.ReadLine();
    if (day is null || day.Length > 2) {
      Console.WriteLine("Invalid day format.");
      return;
    }

    try {
      assignment.Deadline = new DateOnly(int.Parse(year), int.Parse(month), int.Parse(day));
    }
    catch (ArgumentOutOfRangeException) {
      Console.WriteLine("Invalid date format. Nothing modified.");
      return;
    }
    Console.WriteLine("New deadline set!");
  }
}