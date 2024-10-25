using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "1.0,25.0,0.1")] public float speed = GameConstants.DEFAULT_SPEED;

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        CheckForDashInput();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new Vector3(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= speed;
        characterNode.FlipSprite();
        characterNode.MoveAndSlide();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
