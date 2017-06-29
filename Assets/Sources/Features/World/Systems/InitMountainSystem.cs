using Entitas;
using UnityEngine;

public sealed class InitMountainSystem : IInitializeSystem, CellRenderer<WorldEntity> {
  private readonly WorldContext context;
  private readonly Config config;
  private QuadTree<WorldEntity> terrain;

  public InitMountainSystem(Contexts contexts) {
    context = contexts.world;
    config = contexts.game.config.value;
  }

  public void Initialize() {
    var mapSize = config.mapSize;

    terrain = new QuadTree<WorldEntity>(this, new Rect(0, 0, mapSize, mapSize));

    for (var y = 0; y < mapSize; y++) {
      for (var x = 0; x < mapSize; x++) {
        if (Height(x, y) > .5f) {
          terrain.Insert(new Vector2(x, y));
          var tile = context.tileMap.tiles.Get(x, y);
          tile.hasMountain = true;
        }
      }
    }

    terrain.Visualize();
  }

  public void RemoveCell(WorldEntity cell) {
    cell.isDestroyed = true;
  }

  public WorldEntity RenderCell(Rect bounds) {
    var mountain = context.CreateEntity();
    var height = Random.Range(1f, 3f);

    mountain.AddPosition(bounds.x + bounds.width / 2f, 0, bounds.y + bounds.height / 2f);
    mountain.AddScale(bounds.width, height, bounds.height);
    mountain.AddAsset("Prefabs/Cube");

    return mountain;
  }

  private float Height(float x, float y) {
    var perlinX = config.seed + 1000 + x / 15f;
    var perlinY = config.seed + 1000 + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }
}
