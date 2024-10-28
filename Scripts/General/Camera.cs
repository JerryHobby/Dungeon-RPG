using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] public Node target;
    [Export] public Vector3 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleStartGame()
    {
        Reparent(target);
        Position = positionFromTarget;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }
}
