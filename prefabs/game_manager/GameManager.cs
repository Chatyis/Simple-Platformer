using Godot;
using System;

public partial class GameManager : Node2D
{
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			GetTree().Paused = !GetTree().Paused;
		}
	}
}
