using System.Collections.Generic;

public class PriorityQueue<V, P> {
  private SortedDictionary<P, LinkedList<V>> items;
  public bool IsEmpty {
    get { return items.Count <= 0; }
  }

  public PriorityQueue() {
    items = new SortedDictionary<P, LinkedList<V>>();
  }

  public void Enqueue(V value, P priority) {
    LinkedList<V> values;

    if (!items.TryGetValue(priority, out values)) {
      values = new LinkedList<V>();
      items.Add(priority, values);
    }

    values.AddLast(value);
  }

  public V Dequeue() {
    SortedDictionary<P, LinkedList<V>>.KeyCollection.Enumerator enumerator = items.Keys.GetEnumerator();
    enumerator.MoveNext();

    P priority = enumerator.Current;
    LinkedList<V> values = items[priority];
    V value = values.First.Value;

    values.RemoveFirst();

    if (values.Count == 0) {
      items.Remove(priority);
    }

    return value;
  }
}
