using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderScaleSystem : ReactiveSystem<GameEntity> {
  public RenderScaleSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Scale);
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasView && entity.hasScale;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      entity.view.gameObject.transform.localScale = new Vector3(entity.scale.x, entity.scale.y, entity.scale.z);
    });
  }
}
