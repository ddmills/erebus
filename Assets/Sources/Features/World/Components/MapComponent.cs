using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class MapComponent : IComponent {
  public Map value;
}
