using System.Collections.Generic;
using UnityEngine;
using Entitas;

public sealed class FollowPathSystem : ReactiveSystem<GameEntity> {
  private readonly Config config;
  public FollowPathSystem(Contexts contexts) : base(contexts.game) {
    config = contexts.game.config.value;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AllOf(
      GameMatcher.Path,
      GameMatcher.AbleToMove
    ));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasPath && entity.isAbleToMove;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      var goal = entity.path.tiles[entity.path.currentNodeIndex];
      var x = goal.X * config.tileSize + config.tileSize * .5f;
      var z = goal.Y * config.tileSize + config.tileSize * .5f;

      entity.ReplaceMoveTo(x, 0, z, .25f);
    });
  }
}
