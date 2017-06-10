using Entitas;

public sealed class ViewSystems : Feature {
  public ViewSystems(Contexts contexts) : base("Init Systems") {
    Add(new AddViewSystem(contexts));
    Add(new RemoveViewSystem(contexts));
    Add(new RenderPositionSystem(contexts));
  }
}
