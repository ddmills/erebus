using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CompleteTasksSystem : ReactiveSystem<TaskEntity> {
  private GameContext gameContext;
  public CompleteTasksSystem(Contexts contexts) : base(contexts.task) {
    gameContext = contexts.game;
  }

  protected override ICollector<TaskEntity> GetTrigger(IContext<TaskEntity> context) {
    return context.CreateCollector(TaskMatcher.Completed);
  }

  protected override bool Filter(TaskEntity entity) {
    return entity.isCompleted;
  }

  protected override void Execute(List<TaskEntity> entities) {
    entities.ForEach(task => {
      task.processor.value.OnComplete(task);
      task.workers.ids.ForEach(id => {
        var worker = gameContext.GetEntityWithId(id);
        task.processor.value.OnRemoveWorker(worker, task);
        worker.RemoveTask();
      });
      task.ReplaceWorkers(new List<int>());
      task.Destroy();
    });
  }
}
