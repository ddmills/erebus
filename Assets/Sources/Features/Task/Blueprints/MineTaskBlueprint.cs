using System.Collections.Generic;
using Entitas;

public sealed class MineTaskBlueprint {
  private static TaskProcessor processor = new MiningProcessor();
  public static TaskEntity Create(int x, int z) {
    TaskEntity task = Contexts.sharedInstance.task.CreateEntity();
    task.AddType(TaskType.Mine);
    task.AddProcessor(processor);
    task.AddVerb("mine", "mined", "mining");
    task.AddWorkers(new List<int>());
    task.AddPosition(x, 0, z);
    task.isCompleted = false;
    return task;
  }
}
