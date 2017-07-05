using Entitas;
using UnityEngine;

public sealed class InitMountainSystem : IInitializeSystem {
  private readonly Map map;
  private readonly Config config;

  public InitMountainSystem(Contexts contexts) {
     map = contexts.game.map.value;
     config = contexts.game.config.value;
  }

  public void Initialize() {
    for (var y = 0; y < map.Size; y++) {
      for (var x = 0; x < map.Size; x++) {
        if (Height(x, y) > .5f) {
          map.AddMountain(x, y);
        }
      }
    }
    map.mountains.Render();
  }

  private float Height(float x, float y) {
    var perlinX = config.seed + 1000 + x / 15f;
    var perlinY = config.seed + 1000 + y / 15f;
    return Mathf.PerlinNoise(perlinX, perlinY);
  }
}
