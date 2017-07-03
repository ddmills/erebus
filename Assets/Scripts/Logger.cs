using UnityEngine;

public sealed class Logger {
  public static void Log(object message) {
    Debug.Log(message);
  }

  public static void Error(object message) {
    Debug.LogError(message);
  }
}
