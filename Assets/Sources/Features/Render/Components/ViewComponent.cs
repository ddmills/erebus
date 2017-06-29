using Entitas;
using UnityEngine;

[Game, World]
public sealed class ViewComponent : IComponent {
  public GameObject gameObject;
}
