using System.Collections.Generic;
using Entitas;

public sealed class WanderTaskBlueprint {
  public static TaskEntity Create() {
    TaskEntity task = Contexts.sharedInstance.task.CreateEntity();
    task.AddType(TaskType.Wander);
    // task.AddWanderTask(4f, 0f);
    task.AddVerb("wander", "wandered", "wandering");
    task.AddRange(3f, 6f);
    task.AddWorkers(new List<int>());
    task.isCompleted = false;
    return task;
  }
}
