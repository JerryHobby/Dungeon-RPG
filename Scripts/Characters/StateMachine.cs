using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        Node newState = null;

        foreach (Node state in states)
        {
            if (state is T)
            {
                newState = state;
                break;
            }
        }
        if (newState == null) return;
        currentState = newState;
        currentState.Notification(5001);
    }
}
