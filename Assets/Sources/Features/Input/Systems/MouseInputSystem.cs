using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class MouseInputSystem : IExecuteSystem {
  private readonly InputContext context;

  public MouseInputSystem(Contexts contexts) {
    context = contexts.input;
  }

  public void Execute() {

  }
}
