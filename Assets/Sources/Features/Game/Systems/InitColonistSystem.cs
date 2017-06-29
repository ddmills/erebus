using Entitas;

public sealed class InitColonistSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitColonistSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    ColonistBlueprint.Create(1, 1);
    ColonistBlueprint.Create(4, 4);
    ColonistBlueprint.Create(2, 5);
  }
}
