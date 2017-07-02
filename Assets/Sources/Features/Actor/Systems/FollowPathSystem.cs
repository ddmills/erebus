using System.Collections.Generic;
using UnityEngine;
using Entitas;

public sealed class FollowPathSystem : ReactiveSystem<GameEntity> {
  public FollowPathSystem(Contexts contexts) : base(contexts.game) {
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
      Debug.Log("start movin");
    });
  }
}
