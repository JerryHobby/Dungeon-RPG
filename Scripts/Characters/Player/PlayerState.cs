using Godot;
using System;

public abstract partial class PlayerState : CharacterState
{
    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachine.SwitchState<PlayerAttackState>();
        }
    }

    protected void CheckForDashInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachine.SwitchState<PlayerDashState>();
        }
    }
}
