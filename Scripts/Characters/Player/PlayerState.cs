using Godot;
using System;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

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


    private void HandleZeroHealth()
    {
        characterNode.StateMachine.SwitchState<PlayerDeathState>();
    }
}
