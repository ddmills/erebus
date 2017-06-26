using System;
using System.Collections.Generic;
using Entitas;

public sealed class WorkerAddedSystem : ReactiveSystem<TaskEntity>, ICleanupSystem {
  private readonly GameContext context;

  public WorkerAddedSystem(Contexts contexts) : base(contexts.task) {
    context = contexts.game;
  }

  protected override ICollector<TaskEntity> GetTrigger(IContext<TaskEntity> context) {
    return context.CreateCollector(TaskMatcher.WorkerAdded);
  }

  protected override bool Filter(TaskEntity entity) {
    return entity.hasWorkerAdded && entity.hasWorkers;
  }

  protected override void Execute(List<TaskEntity> entities) {
    entities.ForEach(task => {
      var worker = context.GetEntityWithId(task.workerAdded.id);
      task.taskProcessor.value.OnAddWorker(worker, task);
    });
  }

  public void Cleanup() {

  }
}
