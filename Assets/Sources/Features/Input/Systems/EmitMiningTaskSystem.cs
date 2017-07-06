using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class EmitMiningTaskSystem : ReactiveSystem<InputEntity> {
  private readonly InputContext context;
  private Camera camera;
  private Map map;

  public EmitMiningTaskSystem(Contexts contexts) : base(contexts.input) {
    context = contexts.input;
    camera = Camera.main;
    map = contexts.game.map.value;
  }

  protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
    return context.CreateCollector(
      InputMatcher.AllOf(
        InputMatcher.MouseUp,
        InputMatcher.LeftMouse
      )
    );
  }

  protected override bool Filter(InputEntity entity) {
    return entity.isLeftMouse && entity.hasMouseUp;
  }

  protected override void Execute(List<InputEntity> entities) {
    var lmb = context.leftMouseEntity;
    Vector3 position = new Vector3(lmb.mouseUp.x, lmb.mouseUp.y, 0);
    RaycastHit hit;
    Ray ray = camera.ScreenPointToRay(position);

    if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Mountain"))) {
      Transform ob = hit.transform;

      var x = Mathf.FloorToInt(hit.point.x);
      var y = Mathf.FloorToInt(hit.point.z);
      if (map.GetTile(x, y).hasMountain) {
        MineTaskBlueprint.Create(x, y);
      }
    }
  }
}
