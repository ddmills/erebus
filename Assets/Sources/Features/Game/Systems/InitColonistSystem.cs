using System;
using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitColonistSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    createColonist(1, 1);
    createColonist(4, 4);
    createColonist(2, 5);
  }

  private Entity createColonist(float x, float z) {
    var colonist = context.CreateEntity();
    colonist.AddAsset("Prefabs/Colonist");
    colonist.AddPosition(x, 0, z);
    return colonist;
  }
}
