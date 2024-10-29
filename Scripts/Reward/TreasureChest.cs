using Godot;
using System;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource reward;

    public override void _Ready()
    {
        spriteNode.Hide();

        areaNode.BodyEntered += (body) => spriteNode.Show();
        areaNode.BodyExited += (body) => spriteNode.Hide();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)
        && areaNode.GetOverlappingBodies().Count > 0)
        {
            GameEvents.RaiseReward(reward);
            QueueFree();
        }
    }
}
