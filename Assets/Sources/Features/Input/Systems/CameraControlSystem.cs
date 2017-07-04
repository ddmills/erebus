using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class CameraControlSystem : ReactiveSystem<InputEntity> {
  private readonly InputContext context;
  private Camera camera;
  private Transform cameraHandle;
  private GameEntity timeEntity;
  private float panSpeed;

  public CameraControlSystem(Contexts contexts) : base(contexts.input) {
    context = contexts.input;
    camera = Camera.main;
    cameraHandle = camera.transform.parent;
    timeEntity = contexts.game.timeEntity;
    panSpeed = 4;
  }

  protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
    return context.CreateCollector(
      InputMatcher.AllOf(
        InputMatcher.MousePosition,
        InputMatcher.MiddleMouse
      )
    );
  }

  protected override bool Filter(InputEntity entity) {
    return entity.isMiddleMouse && entity.hasMousePosition;
  }

  protected override void Execute(List<InputEntity> entities) {
    var mmb = context.middleMouseEntity;
    var mouseOffsetX = mmb.mousePosition.x - mmb.mousePosition.previousX;
    var mouseOffsetY = mmb.mousePosition.y - mmb.mousePosition.previousY;

    Vector3 direction = new Vector3(cameraHandle.forward.x, 0, cameraHandle.forward.z);
    direction.Normalize();

    Vector3 forward = direction * timeEntity.time.delta * panSpeed * mouseOffsetY;
    Vector3 right = Vector3.right * timeEntity.time.delta * panSpeed * mouseOffsetX;

    cameraHandle.Translate(right, Space.Self);
    cameraHandle.Translate(forward, Space.World);
  }
}
