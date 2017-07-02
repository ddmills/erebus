using System;
using System.Collections.Generic;
using Entitas;

public sealed class PathfindingSystem : ReactiveSystem<GameEntity> {
  private readonly PathFinder<Tile> pathFinder;
  private readonly TileMap<Tile> tiles;
  private readonly Config config;

  public PathfindingSystem(Contexts contexts) : base(contexts.game) {
    tiles = contexts.game.tileMap.tiles;
    config = contexts.game.config.value;
    pathFinder = new PathFinder<Tile>(tiles);
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Goal);
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasGoal && entity.hasPosition;
  }

  protected override void Execute(List<GameEntity> entities) {
    entities.ForEach(entity => {
      var goalX = (int) Math.Floor(entity.goal.x / config.tileSize);
      var goalY = (int) Math.Floor(entity.goal.z / config.tileSize);

      var startX = (int) Math.Floor(entity.position.x / config.tileSize);
      var startY = (int) Math.Floor(entity.position.z / config.tileSize);

      var goal = tiles.Get(goalX, goalY);
      var start = tiles.Get(startX, startY);

      if (start != null && goal != null) {
        var path = pathFinder.Find(start, goal, (from, to) => {
          if (to.hasMountain) {
            return -1f;
          }

          if (from.X != to.X && from.Y != to.Y) {
            return 1.4142f;
          }

          return 1f;
        });

        entity.ReplacePath(path, 0);
      }
    });
  }
}
