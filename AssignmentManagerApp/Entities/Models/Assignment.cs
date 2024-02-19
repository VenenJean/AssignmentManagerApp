using Entities.Abstraction;

namespace Entities.Models;

public class Assignment(Guid id, int position, bool status, string name, DateOnly deadline) : Entity(id) {
  public string Name { get; set; } = name;
  public int Position { get; set; } = position;
  public bool Status { get; set; } = status;
  public DateOnly Deadline { get; set; } = deadline;

  public User User { get; set; } = null!;
}