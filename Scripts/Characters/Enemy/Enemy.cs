using Godot;
using System;

public partial class Enemy : Character
{
    [Export(PropertyHint.Range, "0,100")] public float Speed = GameConstants.DEFAULT_ENEMY_SPEED;

}
