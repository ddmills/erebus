public sealed class MiningProcessor : TaskProcessor {
  public override void OnAddWorker(GameEntity worker, TaskEntity task) {
    MoveToMountain(worker, task);
  }

  public override void Process(GameEntity worker, TaskEntity task) {
    if (worker.isGoalReached) {
      task.ReplaceProgress(0, 2f);
    }
    if (task.hasProgress) {
      Mine(worker, task);
    }
  }
  public override bool CanBeWorkedBy(GameEntity worker, TaskEntity task) {
    return worker.isAbleToMove && (task.workers.ids.Count == 0 || task.workers.ids.Contains(worker.id.value));
  }

  private void MoveToMountain(GameEntity worker, TaskEntity task) {
    var goalX = task.position.x;
    var tiles = Contexts.sharedInstance.game.tileMap.tiles;

    var tile = tiles.Get((int) task.position.x, (int) task.position.z);
    Tile nearby = null;

    foreach (var neighbor in tiles.Neighbors(tile)) {
      if (!neighbor.hasMountain) {
        nearby = neighbor;
        break;
      }
    }

    if (nearby != null) {
      worker.ReplaceGoal(nearby.X, nearby.Y);
    }
  }

  private void Mine(GameEntity worker, TaskEntity task) {
    var current = task.progress.current + Contexts.sharedInstance.game.time.delta;
    task.ReplaceProgress(current, task.progress.max);
    if (task.progress.current >= task.progress.max) {
      task.isCompleted = true;
      task.RemoveProgress();
    }
  }
}
