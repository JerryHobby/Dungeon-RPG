using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DEATH);
        characterNode.AnimPlayerNode.AnimationFinished += OnAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.Stop();
        characterNode.AnimPlayerNode.AnimationFinished -= OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animName)
    {
        characterNode.QueueFree();
    }
}
