using Entitas;

public sealed class TaskSystems : Feature {
  public TaskSystems(Contexts contexts) : base("Task Systems") {
    Add(new AssignProcessorSystem(contexts));
    Add(new WorkerAddedSystem(contexts));
    Add(new ProcessTasksSystem(contexts));
  }
}
