using System;
using System.Collections.Generic;
using Entitas;

public sealed class AssignProcessorSystem : ReactiveSystem<TaskEntity> {
  private readonly Dictionary<TaskType, TaskProcessor> processors;

  public AssignProcessorSystem(Contexts contexts) : base(contexts.task) {
    processors = new Dictionary<TaskType, TaskProcessor>();
    processors.Add(TaskType.Wander, new WanderProcessor());
  }

  protected override ICollector<TaskEntity> GetTrigger(IContext<TaskEntity> context) {
    return context.CreateCollector(TaskMatcher.Type);
  }

  protected override bool Filter(TaskEntity entity) {
    return entity.hasType && !entity.hasTaskProcessor;
  }

  protected override void Execute(List<TaskEntity> entities) {
    entities.ForEach(task => {
      TaskProcessor processor;
      processors.TryGetValue(task.type.value, out processor);
      task.AddTaskProcessor(processor);
    });
  }
}
