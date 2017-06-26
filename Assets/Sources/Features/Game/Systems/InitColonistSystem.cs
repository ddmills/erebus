using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class InitColonistSystem : IInitializeSystem {
  private readonly GameContext context;

  public InitColonistSystem(Contexts contexts) {
    context = contexts.game;
  }

  public void Initialize() {
    createColonist(1, 1);
    createColonist(4, 4);
    createColonist(2, 5);
  }

  private Entity createColonist(float x, float z) {
    TaskEntity task = WanderTaskBlueprint.Create();
    var colonist = context.CreateEntity();
    colonist.AddAsset("Prefabs/Colonist");
    colonist.AddPosition(x, 0, z);
    colonist.AddSpeed(Random.Range(.3f, 4f));
    colonist.isOwnedByPlayer = true;
    colonist.isWorker = true;
    colonist.AddTask(task.id.value);
    task.workers.ids.Add(colonist.id.value);
    task.AddWorkerAdded(colonist.id.value);
    task.ReplaceWorkers(task.workers.ids);
    return colonist;
  }
}
