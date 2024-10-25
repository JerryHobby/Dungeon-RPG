using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        GD.Print("EnemyAttackState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);

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
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
    }

    private void PerformHit()
    {
        Vector3 newPosition = characterNode.SpriteNode.FlipH ?
            Vector3.Left : Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        characterNode.HitboxNode.Position = newPosition;
        characterNode.EnableHitbox(true);

        //GD.Print("Enemy PerformHit");
    }
}
