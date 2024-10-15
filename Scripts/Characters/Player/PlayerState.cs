using Godot;
using System;

public abstract partial class PlayerState : Node
{
    protected Player characterNode;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
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
}
