using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input, Unique]
public sealed class MouseComponent : IComponent {
  public bool leftMouseDown = false;
  public bool middleMouseDown = false;
  public bool rightMouseDown = false;
}
