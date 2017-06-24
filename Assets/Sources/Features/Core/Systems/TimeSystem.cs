using Entitas;
using UnityEngine;

public sealed class TimeSystem : IExecuteSystem {
  private readonly GameContext context;

  public TimeSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Execute() {
    var scale = context.time.scale;
    var delta = Time.deltaTime * scale;

    context.ReplaceTime(delta, scale);
  }
}
