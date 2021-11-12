using Godot;
using System;

public class Item : Area2D
{
    [ExportEnumAsFlags(typeof(ItemTypes))]
    public ItemTypes Type = ItemTypes.Reset;

    [Export]
    Color ItemColor = new Color();

    Sprite sprite;

    public override void _Ready()
    {
        sprite = GetNode<Sprite>("Sprite");
        sprite.Modulate = ItemColor;
    }

    void OnItemBodyEntered(PhysicsBody2D body)
    {
        GD.PrintS("OnItemBodyEntered called");
        if (body.Name == "Player")
        {
            var player = (Player)body;
            player.UseItem(this);
            QueueFree();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        sprite.Modulate = ItemColor;
    }
}
