using Godot;
using System;

public class Tile : StaticBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    public Sprite BlockSprite;

    [Export]
    Texture Texture;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BlockSprite = (Sprite)GetNode("BlockSprite");
        if (BlockSprite != null && Texture != null)
        {
            BlockSprite.Texture = Texture;
        }

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
