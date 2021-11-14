using Godot;
using System;

public class SceneTransition : ColorRect
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextScene;

    public AnimationPlayer animationPlayer;

    [Export]
    public string Animation = "Fade";

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.PlayBackwards(Animation);
    }

    public void TransitionToNextScene(String sceneName)
    {
        animationPlayer.Play(Animation);
        GetTree().ChangeScene(sceneName);
    }
}
