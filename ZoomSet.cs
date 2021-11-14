using Godot;
using System;

public class ZoomSet : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public Vector2 CameraScaleOnEnter;

    [Export]
    public Vector2 CameraScaleOnExit;


    public override void _Ready()
    {
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    void OnZoomSetBodyEntered(PhysicsBody2D body)
    {
        GD.Print("OnZoomSetBodyEntered");
        if (body.Name == "Player")
        {
            ((Player)body).SetCameraZoom(CameraScaleOnExit);
        }
    }

    void OnZoomSetBodyExited(PhysicsBody2D body)
    {
        GD.Print("OnZoomSetBodyExited");
        if (body.Name == "Player")
        {
            ((Player)body).SetCameraZoom(CameraScaleOnExit);
        }
    }
}
