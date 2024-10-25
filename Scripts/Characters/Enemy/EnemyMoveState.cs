using Godot;
using System;

public partial class EnemyMoveState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
