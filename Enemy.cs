using Godot;
using System;

public class Enemy : Area2D
{

    [Export]
    public int Speed = 100;

    [Export]
    public NodePath PatrolPath;
    private int patrolIndex = 0;
    private Vector2 Velocity = new Vector2();

    // [Export]
    // public int MoveDistance = 100;

    // public float StartX;

    // [Export]
    // public float TargetX;


    public override void _Ready()
    {
        //StartX = this.Position.x;
        //TargetX = this.Position.x + MoveDistance;
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
        //https://docs.godotengine.org/en/stable/classes/class_path2d.html
        //https://kidscancode.org/godot_recipes/kyn/path2d/        
        //https://kidscancode.org/godot_recipes/basics/getting_nodes/    
        //https://kidscancode.org/godot_recipes/ai/path_follow/
        //var posX = MoveTo(Position.x, TargetX, Speed * delta);
        if(PatrolPath!=null)
        {            
            //Path2D path            
            var node = GetNode(PatrolPath);
            //node.ge
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
}

