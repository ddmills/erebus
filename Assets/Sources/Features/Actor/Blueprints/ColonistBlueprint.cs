using Entitas;
using UnityEngine;

public sealed class ColonistBlueprint {
  public static GameEntity Create(float x, float z) {
    var colonist = Contexts.sharedInstance.game.CreateEntity();
    colonist.AddAsset("Prefabs/Colonist");
    colonist.AddPosition(x, 0, z);
    colonist.AddSpeed(Random.Range(.3f, 4f));
    colonist.AddGoal(0f, 0f);
    colonist.isOwnedByPlayer = true;
    colonist.isWorker = true;
    return colonist;
  }
}
