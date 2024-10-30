using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Lightning : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += (StringName animName) => QueueFree();
    }
}
