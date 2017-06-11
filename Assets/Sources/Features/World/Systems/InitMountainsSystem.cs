using Entitas;
using UnityEngine;

public sealed class InitMountainSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitMountainSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    var mountain = context.CreateEntity();
    mountain.AddAsset("Prefabs/Cube");
    mountain.AddPosition(1, 0, 1);
    mountain.isSnapToTile = true;
  }
}
