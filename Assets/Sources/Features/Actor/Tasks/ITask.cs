using Entitas;

public abstract class Task {
  public virtual void Process(GameEntity entity) {}
  public virtual void OnAddWorker(GameEntity entity) {}
  public abstract bool CanBeWorkedBy(GameEntity entity);
}
