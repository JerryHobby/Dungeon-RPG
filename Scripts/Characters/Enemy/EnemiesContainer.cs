using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        ChildExitingTree += HandleChildExitingTree;

        int totalEnemies = GetChildCount();
        GameEvents.RaiseEnemyCount(totalEnemies);
    }

    public void HandleChildExitingTree(Node node)
    {
        int totalEnemies = GetChildCount() - 1;
        GameEvents.RaiseEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }
}
