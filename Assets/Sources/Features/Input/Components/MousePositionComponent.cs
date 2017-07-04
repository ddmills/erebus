using Entitas;

[Input]
public sealed class MousePositionComponent : IComponent {
  public float x;
  public float y;
  public float previousX;
  public float previousY;
}
