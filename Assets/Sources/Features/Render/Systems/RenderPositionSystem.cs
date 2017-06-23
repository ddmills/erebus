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
    entities.ForEach(entity => {
      var position = entity.position;
      if (entity.isSnappedToTile) {
        var tileSize = (float) Contexts.sharedInstance.game.config.value.tileSize;

        var snappedPosition = new Vector3(
          Mathf.Floor(position.x / tileSize) + tileSize / 2,
          position.y,
          Mathf.Floor(position.z / tileSize) + tileSize / 2
        );

        entity.view.gameObject.transform.position = snappedPosition;
      } else {
        entity.view.gameObject.transform.position = new Vector3(position.x, position.y, position.z);
      }
    });
  }
}
