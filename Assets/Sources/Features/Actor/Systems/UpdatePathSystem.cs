using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public sealed class UpdatePathSystem : ReactiveSystem<GameEntity>, ICleanupSystem {
  private readonly IGroup<GameEntity> completed;

  public UpdatePathSystem(Contexts contexts) : base(contexts.game) {
    completed = contexts.game.GetGroup(GameMatcher.GoalReached);
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AllOf(
      GameMatcher.MoveToCompleted,
      GameMatcher.Path
    ));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.isMoveToCompleted && entity.hasPath;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      var currentIndex = entity.path.currentNodeIndex;

      if (currentIndex + 1 >= entity.path.tiles.Count) {
        entity.RemovePath();
        entity.isGoalReached = true;
        return;
      }

      entity.ReplacePath(entity.path.tiles, currentIndex + 1);
    });
  }

  public void Cleanup() {
    foreach (var entity in completed.GetEntities()) {
      entity.isGoalReached = false;
    }
  }
}
