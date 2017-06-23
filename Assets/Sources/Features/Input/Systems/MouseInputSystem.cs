using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class MouseInputSystem : IExecuteSystem {
  private readonly InputContext context;

  public MouseInputSystem(Contexts contexts) {
    context = contexts.input;
  }

  public void Execute() {
    var lmb = Input.GetMouseButton(0);
    var rmb = Input.GetMouseButton(1);
    var mmb = Input.GetMouseButton(2);

    context.ReplaceMouse(lmb, mmb, rmb);
  }
}
