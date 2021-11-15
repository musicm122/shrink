using Godot;
using System;

public class Doorway : Area2D
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextScene;

    SceneTransition sceneTransition;

    public override void _Ready()
    {
        sceneTransition = GetNode<SceneTransition>("SceneTransition");
    }

    void OnDoorwayBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "Player")
        {
            sceneTransition.TransitionToNextScene(NextScene);
        }
    }
}
