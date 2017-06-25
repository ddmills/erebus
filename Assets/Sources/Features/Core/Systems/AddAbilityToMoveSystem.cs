using System;
using System.Collections.Generic;
using Entitas;

public sealed class AddAbilityToMoveSystem : ReactiveSystem<GameEntity> {
  public AddAbilityToMoveSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(
      GameMatcher.AllOf(
        GameMatcher.Position,
        GameMatcher.Speed
      )
    );
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasPosition && entity.hasSpeed && entity.speed.value > 0;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      entity.isAbleToMove = true;
    });
  }
}
