using Godot;
using System;

public class HUD : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ((Button)FindNode("StartButton")).GrabFocus();
    }

    void OnStartButtonPressed()
    {
        try
        {
            GetTree().ChangeScene("res://Main.tscn");
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
