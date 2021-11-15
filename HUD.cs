using Godot;
using System;

public class HUD : CanvasLayer
{

    [Export(PropertyHint.File, "*.tscn")]
    public string NewGameScene = "res://L1.tscn";

    public override void _Ready()
    {
        ((Button)FindNode("StartButton")).GrabFocus();
    }

    void OnStartButtonPressed()
    {
        try
        {
            GetTree().ChangeScene(NewGameScene);
        }
        catch (Exception e)
        {
            GD.PrintS(e.Message);
            GD.PrintErr(e);
            throw e;
        }
    }
    void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
