using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject {
  public int seed;
}
