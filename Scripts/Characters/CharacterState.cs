using Godot;
using System;

public abstract partial class CharacterState : Node
{
    protected Character characterNode;
    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        EnableNode(false);
    }


    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            EnableNode(true);
        }
        if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            EnableNode(false);
            ExitState();
        }
    }


    private void EnableNode(bool enable)
    {
        SetPhysicsProcess(enable);
        SetProcessInput(enable);
    }


    protected virtual void EnterState()
    {
    }


    protected virtual void ExitState()
    {
    }
}
