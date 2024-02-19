using Entities.Abstraction;

namespace Entities.Models;

public class User(Guid id, string name, string password) : Entity(id) {
  public string Name { get; set; } = name;
  public string Password { get; set; } = password;

  public List<Assignment> Assignments { get; } = [];
}