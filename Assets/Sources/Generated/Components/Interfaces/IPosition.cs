//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public interface IPosition {

    PositionComponent position { get; }
    bool hasPosition { get; }

    void AddPosition(float newX, float newY, float newZ);
    void ReplacePosition(float newX, float newY, float newZ);
    void RemovePosition();
}