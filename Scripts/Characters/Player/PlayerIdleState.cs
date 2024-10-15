using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachine.SwitchState<PlayerDashState>();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerMoveState>();
        }
    }

    protected override void EnterState()
    {
        characterNode.animPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
