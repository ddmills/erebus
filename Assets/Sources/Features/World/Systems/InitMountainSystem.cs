using Entitas;
using UnityEngine;

public sealed class InitMountainSystem : IInitializeSystem, CellRenderer<GameEntity> {
  private readonly GameContext context;
  private QuadTree<GameEntity> terrain;

  public InitMountainSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    var mapSize = context.config.value.mapSize;

    terrain = new QuadTree<GameEntity>(this, new Rect(0, 0, mapSize, mapSize));

    for (var y = 0; y < mapSize; y++) {
      for (var x = 0; x < mapSize; x++) {
        if (Height(x, y) > .5f) {
          terrain.Insert(new Vector2(x, y));
        }
      }
    }

    terrain.Visualize();
  }

  public void Remove(GameEntity cell) {
    cell.isDestroyed = true;
  }

  public GameEntity Render(Rect bounds) {
    var mountain = context.CreateEntity();
    var height = Random.Range(1f, 3f);

    mountain.AddPosition(bounds.x + bounds.width / 2f, 0, bounds.y + bounds.height / 2f);
    mountain.AddScale(bounds.width, height, bounds.height);
    mountain.AddAsset("Prefabs/Cube");

    return mountain;
  }

  private float Height(float x, float y) {
    var perlinX = context.config.value.seed + 1000 + x / 15f;
    var perlinY = context.config.value.seed + 1000 + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }
}
