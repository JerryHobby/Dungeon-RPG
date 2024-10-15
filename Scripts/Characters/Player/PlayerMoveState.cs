using Godot;
using System;

public partial class PlayerMoveState : PlayerState
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
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new Vector3(characterNode.direction.X, 0, characterNode.direction.Y) * characterNode.Speed;
        characterNode.FlipSprite();
        characterNode.MoveAndSlide();
    }

    protected override void EnterState()
    {
        characterNode.animPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
