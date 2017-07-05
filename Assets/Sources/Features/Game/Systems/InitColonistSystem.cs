using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  public InitColonistSystem(Contexts contexts) {
  }

  public void Initialize() {
    ColonistBlueprint.Create(1, 16);
    ColonistBlueprint.Create(3, 15);
    ColonistBlueprint.Create(8, 17);
    MineTaskBlueprint.Create(9, 24);
    MineTaskBlueprint.Create(11, 25);
  }
}
