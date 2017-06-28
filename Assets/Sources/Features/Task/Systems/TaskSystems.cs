using Entitas;

public sealed class TaskSystems : Feature {
  public TaskSystems(Contexts contexts) : base("Task Systems") {
    Add(new AssignTasksSystem(contexts));
    Add(new ProcessTasksSystem(contexts));
    Add(new CompleteTasksSystem(contexts));
  }
}
