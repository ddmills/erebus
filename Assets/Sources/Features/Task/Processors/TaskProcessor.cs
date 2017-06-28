using Entitas;

public abstract class TaskProcessor {
  public virtual void Process(GameEntity worker, TaskEntity task) {}
  public virtual float CalculateWeight(GameEntity worker, TaskEntity task) {
    return 1f;
  }
  public virtual bool CanBeWorkedBy(GameEntity worker, TaskEntity task) {
    return true;
  }
  public virtual void OnAddWorker(GameEntity worker, TaskEntity task) {}
  public virtual void OnRemoveWorker(GameEntity worker, TaskEntity task) {}
  public virtual void OnCancel(TaskEntity task) {}
  public virtual void OnComplete(TaskEntity task) {}
}
