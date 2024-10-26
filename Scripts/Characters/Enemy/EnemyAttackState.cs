using Godot;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    Node3D target = null;

    protected override void EnterState()
    {
        GD.Print("EnemyAttackState");

        BeginAttack();

        characterNode.AnimPlayerNode.AnimationFinished += OnAnimationFinished;
        characterNode.AttackAreaNode.BodyExited += HandleAttackAreaBodyExited;
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.Stop();
        characterNode.AttackAreaNode.BodyExited -= HandleAttackAreaBodyExited;
        characterNode.AnimPlayerNode.AnimationFinished -= OnAnimationFinished;
    }


    private void HandleAttackAreaBodyExited(Node3D body)
    {
        GD.Print("EnemyAttackState: HandleAttackAreaBodyExited");
        characterNode.StateMachine.SwitchState<EnemyChaseState>();
    }


    private void OnAnimationFinished(StringName animName)
    {
        characterNode.EnableHitbox(false);
        BeginAttack();
    }

    private void PerformHit()
    {
        characterNode.HitboxNode.GlobalPosition = target.GlobalPosition;
        characterNode.EnableHitbox(true);
    }

    private void BeginAttack()
    {
        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .FirstOrDefault();

        if (target == null)
        {
            Node3D chaseTarget = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .FirstOrDefault();

            if (chaseTarget == null)
            {
                characterNode.StateMachine.SwitchState<EnemyReturnState>();
            }
            characterNode.StateMachine.SwitchState<EnemyChaseState>();

            return;
        }

        characterNode.SpriteNode.FlipH = target.GlobalPosition.X < characterNode.GlobalPosition.X;

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
    }
}
