using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{

    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float maxIdleTime = 3f;
    private int pointIndex = 0;

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped()) return;

        Move();
    }

    protected override void EnterState()
    {
        GD.Print("EnemyPatrolState");
        // pointIndex = 1;
        // destination = GetPointGlobalPosition(pointIndex);
        // characterNode.AgentNode.TargetPosition = destination;
        HandleIdleTimerTimeout();

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleIdleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }


    protected override void ExitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleIdleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;

        idleTimerNode.Stop();
    }


    private void HandleNavigationFinished()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(1, maxIdleTime);
        idleTimerNode.Start();
    }


    private void HandleIdleTimerTimeout()
    {
        idleTimerNode.Stop();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }
}
