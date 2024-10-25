using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] Timer comboTimerNode;
    private int comboCounter = 0;

    private static string[] attackAnim = new string[]
        { GameConstants.ANIM_KICK, GameConstants.ANIM_ATTACK };

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
        characterNode.AnimPlayerNode.AnimationFinished += OnAnimationFinished;
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.Stop();
        characterNode.AnimPlayerNode.AnimationFinished -= OnAnimationFinished;
        comboTimerNode.Start();
    }


    private void OnAnimationFinished(StringName animName)
    {
        comboCounter = (comboCounter + 1) % attackAnim.Length;

        //  comboCounter++;
        // comboCounter = Mathf.Wrap(comboCounter, 0, attackAnim.Length - 1);

        characterNode.StateMachine.SwitchState<PlayerIdleState>();
    }
}
