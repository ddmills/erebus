using Entitas;

public sealed class ExecutionSystems : Feature {
  public ExecutionSystems(Contexts contexts) : base("Execution Systems") {
    Add(new InputSystems(contexts));
    Add(new MovementSystem(contexts));
  }
}
