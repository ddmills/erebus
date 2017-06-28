using Entitas;
using UnityEngine;

public sealed class WanderProcessor : TaskProcessor {
  public override void OnAddWorker(GameEntity worker, TaskEntity wander) {
    PickNewGoal(worker, wander);
  }

  public override void Process(GameEntity worker, TaskEntity wander) {
    if (worker.isMoveToCompleted) {
      wander.ReplaceProgress(0, 2f);
    }
    if (wander.hasProgress) {
      Idle(worker, wander);
    }
  }

  public override bool CanBeWorkedBy(GameEntity worker, TaskEntity task) {
    return worker.isAbleToMove;
  }

  private void PickNewGoal(GameEntity worker, TaskEntity wander) {
    var x = worker.position.x + Random.Range(-wander.range.max, wander.range.max);
    var z = worker.position.z + Random.Range(-wander.range.max, wander.range.max);
    worker.ReplaceMoveTo(x, 0, z, .5f);
  }

  private void Idle(GameEntity worker, TaskEntity wander) {
    var current = wander.progress.current + Contexts.sharedInstance.game.time.delta;
    wander.ReplaceProgress(current, wander.progress.max);
    if (wander.progress.current >= wander.progress.max) {
      wander.isCompleted = true;
      wander.RemoveProgress();
    }
  }
}
