using Godot;
using System;

public partial class Enemy : Character
{
    [Export(PropertyHint.Range, "0,100")] public float Speed = GameConstants.DEFAULT_ENEMY_SPEED;


    public override void _Ready()
    {
        base._Ready();

        HurtboxNode.AreaEntered += HandleHurtboxAreaEntered;
    }

    private void HandleHurtboxAreaEntered(Area3D area)
    {
        if (area is not IHitbox hitbox) return;

        if (hitbox.CanStun() && GetStatResource(Stat.Health).StatValue > 0)
        {
            StateMachine.SwitchState<EnemyStunState>();
        }
    }
}
