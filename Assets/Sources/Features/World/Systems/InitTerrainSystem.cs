using Entitas;
using UnityEngine;

public sealed class InitTerrainSystem : IInitializeSystem {
  private TerrainGenerator generator;
  private readonly GameContext context;

  public InitTerrainSystem(Contexts contexts) {
    context = contexts.game;
    generator = new TerrainGenerator(
      context.globals.value.seed,
      context.globals.value.mapSize,
      context.globals.value.tileSize
    );
  }

  public void Initialize() {
    var terrain = generator.Generate();
    Terrain.CreateTerrainGameObject(terrain);
  }
}