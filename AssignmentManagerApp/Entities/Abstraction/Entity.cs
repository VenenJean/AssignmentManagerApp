namespace Entities.Abstraction;

public abstract class Entity(Guid id) {
  public Guid Id { get; set; } = id;
}