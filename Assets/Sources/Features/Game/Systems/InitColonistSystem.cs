using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  public InitColonistSystem(Contexts contexts) {
  }

  public void Initialize() {
    ColonistBlueprint.Create(1, 16);
    ColonistBlueprint.Create(56, 22);
    ColonistBlueprint.Create(8, 17);
    MineTaskBlueprint.Create(11, 25);
  }
}
