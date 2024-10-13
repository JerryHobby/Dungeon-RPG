using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private AnimationPlayer animPlayerNode;
    [Export] private Sprite3D spriteNode;
    [ExportGroup("Movement")]
    [Export] private float Speed = GameConstants.DEFAULT_SPEED;
    [Export] private float JumpForce = GameConstants.DEFAULT_JUMP_FORCE;
    [Export] private float Gravity = GameConstants.DEFAULT_GRAVITY;


    private Vector2 direction = new();

    public override void _Ready()
    {
        animPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector3(direction.X, 0, direction.Y) * Speed;

        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.MOVE_LEFT,
            GameConstants.MOVE_RIGHT,
            GameConstants.MOVE_FORWARD,
            GameConstants.MOVE_BACKWARD
    );

        if (direction == Vector2.Zero)
        {
            animPlayerNode.Play(GameConstants.ANIM_IDLE);
        }
        else
        {
            animPlayerNode.Play(GameConstants.ANIM_WALK);
        }
        if (direction.X < 0)
        {
            spriteNode.Scale = GameConstants.FACE_LEFT;
        }
        else if (direction.X > 0)
        {
            spriteNode.Scale = GameConstants.FACE_RIGHT;
        }
    }
}
