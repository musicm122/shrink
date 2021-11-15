using Godot;
using System;

public class Enemy : Area2D
{

    [Export]
    public int Speed = 100;

    [Export]
    public int ChaseSpeed = 150;

    PathFollow2D PathFollow;

    private int patrolIndex = 0;

    private Vector2 Velocity = new Vector2();

    private Vector2[] PatrolPoints;

    Player playerRef;

    public override void _Ready()
    {
        //get_parent().get_node("SiblingName")
        PathFollow = (PathFollow2D)GetParent();
        //PathFollow = (PathFollow2D)FindNode("PathFollow2D");
    }

    void MoveOnPath(float delta)
    {
        var offset = PathFollow.Offset + Speed * delta;
        PathFollow.Offset = offset;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (PathFollow != null)
        {
            MoveOnPath(delta);
        }
        else
        {
            GD.Print("PathFollow is null");
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

