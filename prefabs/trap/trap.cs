using Godot;
using System;

public partial class trap : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	private void OnArea2dBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			var playerScript = new Player();
			playerScript.GameOver();
		}
	}
}
