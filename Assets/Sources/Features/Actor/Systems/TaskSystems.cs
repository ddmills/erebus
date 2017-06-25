using Entitas;

public sealed class TaskSystems : Feature {
  public TaskSystems(Contexts contexts) : base("Task Systems") {
    Add(new WorkerSystem(contexts));
  }
}
