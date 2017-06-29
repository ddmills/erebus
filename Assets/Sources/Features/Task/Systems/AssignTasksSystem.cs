using System.Linq;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AssignTasksSystem : IExecuteSystem {
  private readonly GameContext gameContext;
  private readonly TaskContext taskContext;
  private readonly IGroup<TaskEntity> taskGroup;
  private readonly IGroup<GameEntity> workerGroup;

  public AssignTasksSystem(Contexts contexts) {
    taskContext = contexts.task;
    gameContext = contexts.game;
    taskGroup = taskContext.GetGroup(
      TaskMatcher.AllOf(
        TaskMatcher.Type,
        TaskMatcher.Processor
      )
    );
    workerGroup = gameContext.GetGroup(GameMatcher.Worker);
  }

  public void Execute() {
    var tasks = taskGroup.GetEntities();
    var workers = workerGroup.GetEntities();

    foreach (var worker in workers) {
      AssignTaskToWorker(tasks, worker);
    }
  }

  private void AssignTaskToWorker(TaskEntity[] tasks, GameEntity worker) {
    var task = getHighestPriorityTask(tasks, worker);

    if (worker.hasTask && task != null && worker.task.id == task.id.value) {
      return;
    }

    if (task == null) {
      task = WanderTaskBlueprint.Create(); // TODO: get default for worker
    }

    if (worker.hasTask) {
      var current = taskContext.GetEntityWithId(worker.task.id);
      RemoveWorkerFromTask(worker, current);
    }

    AddWorkerToTask(worker, task);
  }

  private void RemoveWorkerFromTask(GameEntity worker, TaskEntity task) {
      task.workers.ids.Remove(worker.id.value);
      task.ReplaceWorkers(task.workers.ids);
      task.processor.value.OnRemoveWorker(worker, task);
  }

  private void AddWorkerToTask(GameEntity worker, TaskEntity task) {
    worker.ReplaceTask(task.id.value);
    task.workers.ids.Add(worker.id.value);
    task.processor.value.OnAddWorker(worker, task);
    task.ReplaceWorkers(task.workers.ids);
  }

  private TaskEntity getHighestPriorityTask(TaskEntity[] tasks, GameEntity worker) {
    TaskEntity highestPriorityTask = null;
    var highestPriority = 0f;

    foreach (var task in tasks) {
      if (task.processor.value.CanBeWorkedBy(worker, task)) {
        var weight = task.processor.value.CalculateWeight(worker, task);
        if (weight > highestPriority) {
          highestPriorityTask = task;
          highestPriority = weight;
        }
      }
    }

    return highestPriorityTask;
  }
}
