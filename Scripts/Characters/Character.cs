using Godot;
using System;
using System.Linq;


public partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }


    public Vector2 direction = new();


    public override void _Ready()
    {
        if (HurtboxNode != null)
        {
            HurtboxNode.AreaEntered += OnHurtboxAreaEntered;
        }
    }

    private void OnHurtboxAreaEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);
        GD.Print($"{this} health == {health.StatValue}");

    }

    private StatResource GetStatResource(Stat stat)
    {

        foreach (StatResource element in stats)
        {
            if (element.StatType == stat)
            {
                return element;
            }
        }

        throw new Exception($"Stat {stat} not found in stats array of {this}");
    }


    public void FlipSprite()
    {
        if (Velocity.X == 0) return;

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }
}
