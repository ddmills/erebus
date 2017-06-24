using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {
  public Config config;
  Systems systems;

  void Start() {
    Contexts contexts = Contexts.sharedInstance;

    contexts.game.SetConfig(config);
    Random.InitState(config.seed);

    systems = createSystems(contexts);
    systems.Initialize();
  }

  void Update() {
    systems.Execute();
    systems.Cleanup();
  }

  Systems createSystems(Contexts contexts) {
    return new Feature("All Systems")
      .Add(new InitSystems(contexts))
      .Add(new ExecutionSystems(contexts))
      .Add(new ReactiveSystems(contexts));
  }
}
