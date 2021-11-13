using Godot;
using System;

public class Enemy : Area2D
{
    PathFollow2D PathFollow;

    [Export]
    public int Speed = 100;


    [Export]
    public int ChaseSpeed = 150;

    [Export]
    public NodePath PatrolPath;
    private int patrolIndex = 0;
    private Vector2 Velocity = new Vector2();
    private Vector2[] PatrolPoints;

    [Export]
    Player playerRef;

    public override void _Ready()
    {
        PathFollow = GetParent<PathFollow2D>();
        // if (PatrolPath != null)
        // {
        //     PatrolPoints = GetPatrolPoints(PatrolPath);
        // }
    }

    Vector2[] GetPatrolPoints(NodePath patrolPath) => PatrolPoints = ((Path2D)GetNode(patrolPath)).Curve.GetBakedPoints();

    float MoveTo(float currentX, float targetX, float increment)
    {
        var newX = currentX;
        if (newX < targetX)
        {
            newX += increment;
            newX = (newX > targetX) ? increment : newX;
        }
        else
        {
            newX -= increment;
            newX = (newX < targetX) ? targetX : newX;
        }
        return newX;
    }

    void MoveOnPath(float delta)
    {
        PathFollow.Offset = PathFollow.Offset + Speed * delta;
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveOnPath(delta);
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

