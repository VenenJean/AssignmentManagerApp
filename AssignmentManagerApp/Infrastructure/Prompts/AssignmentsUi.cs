using Entities.Models;

namespace Infrastructure.Prompts;

public class AssignmentsUi {
  private List<Assignment> assignments { get; }

  public AssignmentsUi(List<Assignment> assignments) {
    this.assignments = assignments;
  }

  public void PrintAssignments(Func<Assignment, bool>? filter = null) {
    IEnumerable<Assignment> filteredAssignments = assignments;

    if (filter is not null) {
      filteredAssignments = assignments.Where(filter).ToList();
    }

    if (filteredAssignments.Any()) {
      var orderedAssignments = filteredAssignments.OrderBy(a => a.Position);
      foreach (var assignment in orderedAssignments) {
        var uiAssignment = AssignmentToUi(assignment);
        Console.ForegroundColor = uiAssignment.Item2;
        Console.WriteLine(uiAssignment.Item1);
      }
      Console.ResetColor();
      return;
    }
    
    Console.WriteLine("No assignments match the specified criteria.");
  }
  
  private (string, ConsoleColor) AssignmentToUi(Assignment assignment) {
    var assignmentPosition = assignment.Position;
    string uiStatus;
    var assignmentName = assignment.Name;
    var assignmentDeadline = assignment.Deadline;
    string statusMessage;
    ConsoleColor foregroundColor;

    var isCompleted = assignment.Status;
    var isDeadlineExceeded = assignment.Deadline <= new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    if (isCompleted) {
      uiStatus = "[X]";
      statusMessage = isDeadlineExceeded ? "Completed afterwards." : "Completed!";
      foregroundColor = isDeadlineExceeded ? ConsoleColor.DarkGreen : ConsoleColor.Green;
    }
    else {
      uiStatus = isDeadlineExceeded ? "[-]" : "[ ]";
      statusMessage = isDeadlineExceeded ? "Missed!" : "In progress...";
      foregroundColor = isDeadlineExceeded ? ConsoleColor.Red : ConsoleColor.Yellow;
    }
    
    var finalMessage = $"  { assignmentPosition }. { uiStatus } { assignmentName } | { assignmentDeadline } => { statusMessage }";

    return (finalMessage, foregroundColor);
  }
}