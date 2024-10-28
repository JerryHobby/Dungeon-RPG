using Godot;
using System;

public partial class EnemyCountLabel : Label
{
    public override void _Ready()
    {
        GameEvents.OnEnemyCount += HandleEnemyCount;
    }

    private void HandleEnemyCount(int value)
    {
        GD.Print($"Enemy count: {value}");
        Text = value.ToString();
    }
}
