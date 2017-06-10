using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderPositionSystem : ReactiveSystem<GameEntity> {
  public RenderPositionSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Position);
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasView && entity.hasPosition;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      var position = entity.position;
      entity.view.gameObject.transform.position = new Vector3(position.x, position.y, position.z);
    }
  }
}
