using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Input]
public sealed class IdComponent : IComponent {
  [PrimaryEntityIndex] public int value;
}
