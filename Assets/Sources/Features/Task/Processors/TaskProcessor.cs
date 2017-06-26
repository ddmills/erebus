using Entitas;

public abstract class TaskProcessor {
  public virtual void Process(GameEntity worker, TaskEntity task) {}
  public virtual void CanBeWorkedBy(GameEntity worker, TaskEntity task) {}
  public virtual void OnAddWorker(GameEntity worker, TaskEntity task) {}
  public virtual void OnRemoveWorker(GameEntity worker, TaskEntity task) {}
  public virtual void OnCancel() {}
  public virtual void OnComplete() {}
}
