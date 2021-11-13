using Godot;
using System;

public class FallDeath : Area2D
{
	void OnFallDeathBodyEntered(PhysicsBody2D body)
	{
		if (body.Name == "Player")
		{
			var player = (Player)body;
			player.Die();
		}
	}
}
