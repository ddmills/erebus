using Entitas;

[Task]
public sealed class ProgressComponent : IComponent {
  public float current;
  public float max;
}
