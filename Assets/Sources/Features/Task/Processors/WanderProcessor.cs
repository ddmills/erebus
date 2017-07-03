using Entitas;
using UnityEngine;

public sealed class WanderProcessor : TaskProcessor {
  public override void OnAddWorker(GameEntity worker, TaskEntity wander) {
    PickNewGoal(worker, wander);
  }

  public override void Process(GameEntity worker, TaskEntity wander) {
    if (worker.isGoalReached) {
      wander.ReplaceProgress(0, 2f);
    }
    if (wander.hasProgress) {
      Idle(worker, wander);
    }
  }

  public override bool CanBeWorkedBy(GameEntity worker, TaskEntity task) {
    return worker.isAbleToMove && (task.workers.ids.Count == 0 || task.workers.ids.Contains(worker.id.value));
  }

  private void PickNewGoal(GameEntity worker, TaskEntity wander) {
    var game = Contexts.sharedInstance.game;
    var x = (int) worker.position.x / game.config.value.tileSize;
    var y = (int) worker.position.z / game.config.value.tileSize;

    var tiles = game.tileMap.tiles.InRadius(x, y, 6, tile => !tile.hasMountain);

    // TODO: if tiles is empty, task fails

    var idx = Random.Range(0, tiles.Count);
    var goal = tiles[idx];
    worker.ReplaceGoal(goal.X, goal.Y);
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
