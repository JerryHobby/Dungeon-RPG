using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] Timer comboTimerNode;
    [Export] PackedScene lightningScene;

    private static string[] attackAnim = new string[]
        { GameConstants.ANIM_KICK, GameConstants.ANIM_ATTACK };

    private int comboCounter = 0;
    private int maxComboCount = attackAnim.Length;

    public override void _Ready()
    {
        base._Ready();
        comboTimerNode.Timeout += () => comboCounter = 0;
    }

    public override void _Input(InputEvent @event)
    {
        CheckForDashInput();
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(attackAnim[comboCounter], customSpeed: 1.5f);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        characterNode.HitboxNode.BodyEntered += HandleHitboxBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.Stop();
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitboxNode.BodyEntered -= HandleHitboxBodyEntered;

        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter = (comboCounter + 1) % maxComboCount;

        characterNode.EnableHitbox(false);

        characterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPosition = characterNode.SpriteNode.FlipH ?
            Vector3.Left : Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        characterNode.HitboxNode.Position = newPosition;
        characterNode.EnableHitbox(true);
    }

    private void HandleHitboxBodyEntered(Node3D body)
    {
        GD.Print($"Combo: {comboCounter} MaxComboCount: {maxComboCount}");
        if (comboCounter != maxComboCount - 1)
        {
            return;
        }

        Node3D lightning = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;
    }
}
