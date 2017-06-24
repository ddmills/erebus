using Entitas;

public sealed class InitSystems : Feature {
  public InitSystems(Contexts contexts) : base("Init Systems") {
    Add(new InitTerrainSystem(contexts));
    Add(new InitMountainSystem(contexts));
    Add(new InitColonistSystem(contexts));
  }
}
