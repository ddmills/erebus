using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, CreateAssetMenu]
public sealed class Config : ScriptableObject {
  public int mapSize;
  public int tileSize;
  public int seed;
}
