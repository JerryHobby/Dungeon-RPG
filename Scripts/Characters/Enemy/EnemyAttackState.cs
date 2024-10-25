using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        GD.Print("EnemyAttackState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);

        characterNode.AttackAreaNode.BodyExited += HandleAttackAreaBodyExited;
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.Stop();
        characterNode.AttackAreaNode.BodyExited -= HandleAttackAreaBodyExited;
    }


    private void HandleAttackAreaBodyExited(Node3D body)
    {
        GD.Print("EnemyAttackState: HandleAttackAreaBodyExited");
        characterNode.StateMachine.SwitchState<EnemyChaseState>();
    }
}
