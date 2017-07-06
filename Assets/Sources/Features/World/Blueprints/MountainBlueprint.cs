using Entitas;
using UnityEngine;

public sealed class MountainBlueprint {
  public static GameEntity Create(Rect bounds) {
    var mountain = Contexts.sharedInstance.game.CreateEntity();

    mountain.AddPosition(bounds.x + bounds.width / 2f, 0, bounds.y + bounds.height / 2f);
    mountain.AddScale(bounds.width, 1f, bounds.height);
    mountain.AddAsset("Prefabs/Mountain");

    return mountain;
  }
}
