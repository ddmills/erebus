using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class TimeComponent : IComponent {
  public float delta;
  public float scale;
}
