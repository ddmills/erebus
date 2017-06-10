using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity> {
  public RemoveViewSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return new Collector<GameEntity>(
      new IGroup<GameEntity>[] {
        context.GetGroup(GameMatcher.Asset),
        context.GetGroup(GameMatcher.Destroyed)
      },
      new GroupEvent[] {
        GroupEvent.AddedOrRemoved,
        GroupEvent.Added
      }
    );
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasView && entity.isDestroyed;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      Object.Destroy(entity.view.gameObject);
      entity.RemoveView();
    }
  }
}
