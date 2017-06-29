using UnityEngine;

public interface CellRenderer<T> {
  T RenderCell(Rect bounds);
  void RemoveCell(T cell);
}
