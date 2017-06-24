using Entitas;

[Game]
public sealed class MoveToComponent : IComponent {
  public float x;
  public float y;
  public float z;
  public float tolerance = .5f;
}
