using Godot;
using System;

public class Enemy2 : KinematicBody2D
{


    [Export]
    public int ChaseSpeed = 150;

    [Export]
    public int Speed = 100;

    public NodePath PatrolPath;

    private Player playerRef;
    private PathFollow2D PathFollow;

    private int patrolIndex = 0;

    private Vector2 Velocity = Vector2.Zero;

    private Vector2[] PatrolPoints;

    public override void _Ready()
    {
        PathFollow = GetParent<PathFollow2D>();
        if (PatrolPath != null)
        {
            PatrolPoints = GetPatrolPoints(PatrolPath);
        }
    }

    Vector2[] GetPatrolPoints(NodePath patrolPath) => PatrolPoints = ((Path2D)GetNode(patrolPath)).Curve.GetBakedPoints();

    void ChasePlayer(float delta)
    {
        var direction = Position.DirectionTo(playerRef.Position);
        Velocity = Position.DirectionTo(playerRef.Position) * ChaseSpeed;
        Velocity = MoveAndSlide(Velocity);
    }

    void Patrol(float delta)
    {
        var target = PatrolPoints[patrolIndex];
        if (this.Position.DistanceTo(target) < 1)
        {
            patrolIndex = Mathf.Clamp(patrolIndex + 1, 0, PatrolPoints.Length);
            target = PatrolPoints[patrolIndex];
        }
        Velocity = (target - Position).Normalized() * Speed;
        Velocity = MoveAndSlide(Velocity);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (playerRef != null)
        {
            ChasePlayer(delta);
        }
        else if (PatrolPoints != null)
        {
            Patrol(delta);
        }
        else
        {
            GD.PrintS("Patrol Points on Enemy Null");
        }
    }

    void OnEnemyBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "Player")
        {
            var player = (Player)body;
            player.Die();
        }
    }
    void OnDetectRadiusBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "Player")
        {
            playerRef = (Player)body;
        }
    }
    void OnDetectRadiusBodyExited(PhysicsBody2D body)
    {
        playerRef = null;
    }
}
