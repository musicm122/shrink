using Godot;
using System;

public class EnemyColliderCheck : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    void OnEnemyBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "Player")
        {
            var player = (Player)body;
            player.Die();
        }
    }
}
