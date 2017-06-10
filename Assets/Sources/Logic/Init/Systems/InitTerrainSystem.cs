using Entitas;

public sealed class InitTerrainSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitTerrainSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    for (var i = 0; i < 8; i++) {
      for (var j = 0; j < 8; j++) {
        var entity = context.CreateEntity();
        entity.AddPosition(i, 1, j);
        entity.AddAsset("Prefabs/Cube");
      }
    }
  }
}
