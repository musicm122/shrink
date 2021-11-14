using Godot;
using System;

public class MovingPlatform : Node2D
{
    private const string FollowPropertyName = "Follow";

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Tween tween;

    [Export]
    float IdleDuration;

    [Export]
    public float WaitTime = 1.0f;

    [Export]
    public float Speed = 5.0f;

    [Export]
    public float Weight = 0.075f;

    [Export]
    public Vector2 MoveTo;

    public KinematicBody2D Platform;

    //https://www.youtube.com/watch?v=mBNa8LcAsns

    float UnitSize = 10;

    public Vector2 Follow = Vector2.Zero;

    void InitTween()
    {
        var duration = MoveTo.Length() / Speed * UnitSize;
        var endIdleDuration = duration + IdleDuration * 2;
        tween.InterpolateProperty(
            this,
            property: FollowPropertyName,
            initialVal: Vector2.Zero,
            finalVal: MoveTo,
            duration,
            Tween.TransitionType.Linear,
            Tween.EaseType.InOut,
            IdleDuration);

        tween.InterpolateProperty(
            this,
            property: FollowPropertyName,
            initialVal: MoveTo,
            finalVal: Vector2.Zero,
            duration,
            Tween.TransitionType.Linear,
            Tween.EaseType.InOut,
            endIdleDuration);
        tween.Start();
    }

    public override void _Ready()
    {
        tween = GetNode<Tween>("PlatformTween");
        Platform = GetNode<KinematicBody2D>("Platform");
        InitTween();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Platform.Position = Platform.Position.LinearInterpolate(Follow, Weight);
    }
}
