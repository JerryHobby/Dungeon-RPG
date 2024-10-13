using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animPlayerNode;
    [Export] public Sprite3D spriteNode;
    [Export] public StateMachine stateMachine;

    [ExportGroup("Movement")]
    [Export] private float Speed = GameConstants.DEFAULT_SPEED;
    [Export] private float Gravity = GameConstants.DEFAULT_GRAVITY;


    public Vector2 direction = new();


    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector3(direction.X, 0, direction.Y) * Speed;
        FlipSprite();
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    private void FlipSprite()
    {
        if (Velocity.X == 0) return;

        bool isMovingLeft = Velocity.X < 0;
        spriteNode.FlipH = isMovingLeft;
    }
}
