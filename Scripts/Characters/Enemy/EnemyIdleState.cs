using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    override public void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachine.SwitchState<EnemyReturnState>();
        }
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;

    }
    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
