using Godot;
using System;

public class PauseMenu : Control
{
    void PauseCheck()
    {
        if (Input.IsActionJustPressed(InputConstants.Pause))
        {
            GD.PrintS("Pause button pressed");
            GetTree().Paused = !GetTree().Paused;
        }
    }

    public override void _Process(float delta)
    {
        PauseCheck();
    }
}
