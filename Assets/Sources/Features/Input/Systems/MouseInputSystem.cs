using Entitas;
using UnityEngine;

public sealed class MouseInputSystem : IInitializeSystem, IExecuteSystem {
  private readonly InputContext context;
  private InputEntity leftMouse;
  private InputEntity rightMouse;
  private InputEntity middleMouse;
  private enum Mouse { Left, Right, Middle };

  public MouseInputSystem(Contexts contexts) {
    context = contexts.input;
  }

  public void Initialize() {
    context.isLeftMouse = true;
    context.isRightMouse = true;
    context.isMiddleMouse = true;

    leftMouse = context.leftMouseEntity;
    rightMouse = context.rightMouseEntity;
    middleMouse = context.middleMouseEntity;
  }

  public void Execute() {
    var lmb = Input.GetMouseButton(0);
    var rmb = Input.GetMouseButton(1);
    var mmb = Input.GetMouseButton(2);

    var x = Input.mousePosition.x;
    var y = Input.mousePosition.y;

    PopulateMouseEntity(leftMouse, Mouse.Left, x, y);
    PopulateMouseEntity(rightMouse, Mouse.Right, x, y);
    PopulateMouseEntity(middleMouse, Mouse.Middle, x, y);
  }

  private void PopulateMouseEntity(InputEntity entity, Mouse button, float x, float y) {
    var buttonId = (int) button;

    if (Input.GetMouseButtonDown(buttonId)) {
      entity.ReplaceMouseDown(x, y);
    }
    if (Input.GetMouseButton(buttonId)) {
      entity.ReplaceMousePosition(x, y);
    }
    if (Input.GetMouseButtonUp(buttonId)) {
      entity.ReplaceMouseUp(x, y);
    }
  }
}
