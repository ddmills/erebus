using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  public InitColonistSystem(Contexts contexts) {
  }

  public void Initialize() {
    ColonistBlueprint.Create(1, 1);
    ColonistBlueprint.Create(4, 4);
    ColonistBlueprint.Create(2, 5);
  }
}
