using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {
  public Config config;
  Systems systems;
  private int currentId;

  void Start() {
    currentId = 0;

    Contexts contexts = Contexts.sharedInstance;

    contexts.game.SetConfig(config);
    contexts.game.SetTime(0, 1);
    contexts.game.OnEntityCreated += AddGameId;


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
      .Add(new ReactiveSystems(contexts))
      .Add(new TaskSystems(contexts));
  }

  private void AddGameId(IContext context, IEntity entity) {
    GameEntity gameEntity = entity as GameEntity;
    if (gameEntity != null) {
      gameEntity.AddId(currentId++);
    }
  }
}
