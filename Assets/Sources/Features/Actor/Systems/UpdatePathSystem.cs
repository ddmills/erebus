using System.Collections.Generic;
using UnityEngine;
using Entitas;

public sealed class UpdatePathSystem : ReactiveSystem<GameEntity> {
  public UpdatePathSystem(Contexts contexts) : base(contexts.game) {
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
        return;
      }

      entity.ReplacePath(entity.path.tiles, currentIndex + 1);
    });
  }
}
