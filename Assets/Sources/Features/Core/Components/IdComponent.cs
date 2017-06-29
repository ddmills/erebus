using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Input, Task]
public sealed class IdComponent : IComponent {
  [PrimaryEntityIndex] public int value;
}
