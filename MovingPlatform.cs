using Godot;
using System;

public class MovingPlatform : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Tween tweener;

    [Export]
    public float WaitTime = 1.0f;

    
    [Export]
    public Vector2 MoveTo;
    //https://www.youtube.com/watch?v=mBNa8LcAsns

    public override void _Ready()
    {
        tweener = GetNode<Tween>("PlatformTween");

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
