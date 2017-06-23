using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Collections.Generic;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity> {
  private readonly GameContext context;

  public RemoveViewSystem(Contexts contexts) : base(contexts.game) {
    context = contexts.game;
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
      entity.view.gameObject.Unlink();
      Object.Destroy(entity.view.gameObject);
      entity.RemoveView();
    }
  }
}
