using UnityEngine;

public interface CellRenderer<T> {
  T Render(Rect bounds);
  void Remove(T cell);
}
