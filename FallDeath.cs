using Godot;
using System;

public class FallDeath : Area2D
{
    void OnFallDeathBodyEntered(PhysicsBody2D body)
    {
        GD.PrintS("OnItemBodyEntered called");
        if (body.Name == "Player")
        {
            var player = (Player)body;
            player.Die();
        }
    }
}
