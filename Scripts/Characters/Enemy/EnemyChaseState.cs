using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer ChaseTimerNode;
    private CharacterBody3D target;

    public override void _PhysicsProcess(double delta)
    {
        if (target == null)
        {
            characterNode.StateMachine.SwitchState<EnemyReturnState>();
            return;
        }
        Move();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        ChaseTimerNode.Timeout += HandleChaseTimerTimeout;
        ChaseTimerNode.Start();

        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }


    protected override void ExitState()
    {
        target = null;
        ChaseTimerNode.Stop();
        ChaseTimerNode.Timeout -= HandleChaseTimerTimeout;

        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
    }


    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachine.SwitchState<EnemyAttackState>();
    }


    private void HandleChaseAreaBodyExited(Node3D body)
    {
        target = null;
    }


    private void HandleChaseTimerTimeout()
    {
        // has player died?
        if (!IsInstanceValid(target))
        {
            characterNode.StateMachine.SwitchState<EnemyReturnState>();
            return;
        }

        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }
}
