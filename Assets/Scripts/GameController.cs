using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {
  public Globals globals;
  Systems systems;

  void Start() {
    Contexts contexts = Contexts.sharedInstance;

    contexts.game.SetGlobals(globals);
    Random.InitState(globals.seed);

    systems = createSystems(contexts);
    systems.Initialize();
  }

  void Update() {
    systems.Execute();
  }

  Systems createSystems(Contexts contexts) {
    return new Feature("Systems")
      .Add(new InitSystems(contexts))
      .Add(new ViewSystems(contexts));
  }
}
