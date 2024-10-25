using Godot;
using System;

public abstract partial class EnemyState : CharacterState
{
    [Export(PropertyHint.Range, "1.0,25.0,0.1")] public float speed = GameConstants.DEFAULT_ENEMY_SPEED;

    protected Vector3 destination;


    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 dest;
        Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = characterNode.PathNode.GlobalPosition;
        dest = globalPos + localPos;
        dest.Y = characterNode.GlobalPosition.Y;
        return dest;
    }

    protected void Move()
    {
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
        characterNode.Velocity *= speed;
        characterNode.FlipSprite();
        characterNode.MoveAndSlide();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            characterNode.StateMachine.SwitchState<EnemyChaseState>();
        }
    }
}



