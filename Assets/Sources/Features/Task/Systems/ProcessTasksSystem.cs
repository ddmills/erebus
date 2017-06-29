using System.Linq;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class ProcessTasksSystem : IExecuteSystem {
  private readonly GameContext gameContext;
  private readonly TaskContext taskContext;
  private readonly IGroup<TaskEntity> tasks;

  public ProcessTasksSystem(Contexts contexts) {
    taskContext = contexts.task;
    gameContext = contexts.game;
    tasks = taskContext.GetGroup(
      TaskMatcher.AllOf(
        TaskMatcher.Type,
        TaskMatcher.Processor
      )
    );
  }

  public void Execute() {
    foreach (var task in tasks.GetEntities()) {
      foreach (var id in task.workers.ids) {
        var worker = gameContext.GetEntityWithId(id);
        task.processor.value.Process(worker, task);
      }
    }
  }
}
