using Godot;
using System;

public partial class Player : CharacterBody3D
{
    public int Speed = 1;
    public override void _PhysicsProcess(double delta)
    {
        // base._PhysicsProcess(delta);

        //GD.Print("Player _PhysicsProcess");
    }

    public override void _Input(InputEvent @event)
    {
        Vector2 moveDirection;
        moveDirection = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");

        GD.Print("Player _Input", moveDirection);

        Velocity = new Vector3(moveDirection.X, 0, moveDirection.Y);

        MoveAndCollide(Velocity * Speed);

    }
}
