//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TaskEntity {

    public ProgressComponent progress { get { return (ProgressComponent)GetComponent(TaskComponentsLookup.Progress); } }
    public bool hasProgress { get { return HasComponent(TaskComponentsLookup.Progress); } }

    public void AddProgress(float newCurrent, float newMax) {
        var index = TaskComponentsLookup.Progress;
        var component = CreateComponent<ProgressComponent>(index);
        component.current = newCurrent;
        component.max = newMax;
        AddComponent(index, component);
    }

    public void ReplaceProgress(float newCurrent, float newMax) {
        var index = TaskComponentsLookup.Progress;
        var component = CreateComponent<ProgressComponent>(index);
        component.current = newCurrent;
        component.max = newMax;
        ReplaceComponent(index, component);
    }

    public void RemoveProgress() {
        RemoveComponent(TaskComponentsLookup.Progress);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class TaskMatcher {

    static Entitas.IMatcher<TaskEntity> _matcherProgress;

    public static Entitas.IMatcher<TaskEntity> Progress {
        get {
            if (_matcherProgress == null) {
                var matcher = (Entitas.Matcher<TaskEntity>)Entitas.Matcher<TaskEntity>.AllOf(TaskComponentsLookup.Progress);
                matcher.componentNames = TaskComponentsLookup.componentNames;
                _matcherProgress = matcher;
            }

            return _matcherProgress;
        }
    }
}
