using Entitas;

public sealed class InitTerrainSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitTerrainSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    var mapSize = context.globals.value.mapSize;
    var tileSize = context.globals.value.tileSize;
    for (var i = 0; i < mapSize; i++) {
      for (var j = 0; j < mapSize; j++) {
        var entity = context.CreateEntity();
        entity.AddPosition(i * tileSize, 0, j * tileSize);
        entity.AddAsset("Prefabs/Cube");
      }
    }
  }
}
