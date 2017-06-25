using System;
using Entitas;
using UnityEngine;

public sealed class WorkerSystem : IExecuteSystem {
  private readonly GameContext context;
  private readonly IGroup<GameEntity> workers;

  public WorkerSystem(Contexts contexts) {
    context = contexts.game;
    workers = context.GetGroup(GameMatcher.Task);
  }

  public void Execute() {
    foreach (var entity in workers.GetEntities()) {
    }
  }
}
