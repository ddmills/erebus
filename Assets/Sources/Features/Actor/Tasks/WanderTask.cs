using System;
using UnityEngine;
using Entitas;

public sealed class WanderTask : Task {
  public override void Process(GameEntity entity) {
    if (entity.isMoveToCompleted) {
      Debug.Log("DONE");
    }
  }

  public override bool CanBeWorkedBy(GameEntity entity) {
    return entity.isAbleToMove;
  }

  public override void OnAddWorker(GameEntity entity) {
    entity.ReplaceMoveTo(2f, 0f, 6f, .75f);
  }
}
