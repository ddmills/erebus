using System;
using System.Collections.Generic;
using Entitas;

public sealed class DestroySystem : ReactiveSystem<GameEntity> {
  public DestroySystem(Contexts contexts) : base(contexts.game) {
  }

  protected override bool Filter(GameEntity entity) {
    return entity.isDestroyed;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Destroyed);
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => entity.Destroy());
  }
}
