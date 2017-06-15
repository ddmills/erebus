using Entitas;
using UnityEngine;

public sealed class InitMountainSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitMountainSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    var mapSize = context.config.value.mapSize;
    for (var y = 0; y < mapSize; y++) {
      for (var x = 0; x < mapSize; x++) {
        if (Height(x, y) > .5f) {
          CreateMountain(x, y);
        }
      }
    }
  }

  private void CreateMountain(int x, int y) {
    var mountain = context.CreateEntity();
    mountain.AddAsset("Prefabs/Cube");
    mountain.AddPosition(x, 0, y);
    mountain.isSnappedToTile = true;
  }

  private float Height(float x, float y) {
    var perlinX = context.config.value.seed + 1000 + x / 15f;
    var perlinY = context.config.value.seed + 1000 + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }
}
