using Entitas;

public sealed class ExecutionSystems : Feature {
  public ExecutionSystems(Contexts contexts) : base("Init Systems") {
    Add(new AddViewSystem(contexts));
    Add(new RemoveViewSystem(contexts));
    Add(new RenderPositionSystem(contexts));
    Add(new RenderScaleSystem(contexts));
  }
}
