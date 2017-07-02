using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  public InitColonistSystem(Contexts contexts) {
  }

  public void Initialize() {
    ColonistBlueprint.Create(1, 16);
    ColonistBlueprint.Create(4, 22);
    ColonistBlueprint.Create(2, 17);
  }
}
