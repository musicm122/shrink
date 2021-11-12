using Godot;
using System;

public class Enemy : Area2D
{

    [Export]
    public int Speed = 100;

    [Export]
    public int MoveDistance = 100;

    [Export]
    public float StartX;

    [Export]
    public float TargetX;

    public override void _Ready()
    {
        StartX = this.Position.x;
        TargetX = this.Position.x + MoveDistance;
    }

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

    public override void _PhysicsProcess(float delta)
    {
        var posX = MoveTo(Position.x, TargetX, Speed * delta);
        if (posX == TargetX)
        {
            TargetX = (TargetX == StartX) ? posX + MoveDistance : StartX;
        }
        Position = new Vector2(posX, Position.y);
    }

    void OnEnemyBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "Player")
        {
            var player = (Player)body;
            player.Die();
        }
    }
}

