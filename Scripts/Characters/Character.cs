using Godot;
using System;
using System.Linq;
using System.Reflection.Metadata;


public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public CollisionShape3D HitboxShapeNode { get; private set; }
    [Export] public Timer ShaderTimerNode { get; private set; }


    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();
    private ShaderMaterial shader;


    public override void _Ready()
    {
        shader = (ShaderMaterial)SpriteNode.MaterialOverlay;

        if (HurtboxNode != null)
        {
            HurtboxNode.AreaEntered += HandleHurtboxAreaEntered;
        }

        ShaderTimerNode.Timeout += HandleShaderTimerTimeout;

        SpriteNode.TextureChanged += HandleTextureChanged;
    }

    private void HandleHurtboxAreaEntered(Area3D area)
    {
        if (area is not IHitbox hitbox) return;

        StatResource health = GetStatResource(Stat.Health);
        float damage = hitbox.GetDamage();
        GD.Print($"Dealing Damage: {damage}");

        ShaderTimerNode.Start();
        shader.SetShaderParameter("active", true);

        health.StatValue -= damage;
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where((element) => element.StatType == stat)
        .FirstOrDefault();
    }


    public void FlipSprite()
    {
        if (Velocity.X == 0) return;

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }


    public void EnableHitbox(bool value)
    {
        HitboxShapeNode.Disabled = !value;
    }

    private void HandleTextureChanged()
    {
        shader.SetShaderParameter("tex", SpriteNode.Texture);
    }

    private void HandleShaderTimerTimeout()
    {
        shader.SetShaderParameter("active", false);
    }

}
