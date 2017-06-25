using System;
using System.Collections.Generic;
using Entitas;

public sealed class RemoveAbilityToMoveSystem : ReactiveSystem<GameEntity> {
  public RemoveAbilityToMoveSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(
      GameMatcher.Position.Removed(),
      GameMatcher.Speed.AddedOrRemoved()
    );
  }

  protected override bool Filter(GameEntity entity) {
    return !entity.hasPosition || !entity.hasSpeed || entity.speed.value <= 0;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      entity.isAbleToMove = false;
    });
  }
}
