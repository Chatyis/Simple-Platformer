using Godot;
using Platformer;
using System;

public partial class CoinCollision : Node2D
{
	public Ui ui;
	public override void _Ready()
	{
		ui = GetTree().Root.GetNode<Ui>("Ui");
	}
	private void OnArea2DBodyEntered(Node2D body)
	{
		QueueFree();
		GlobalVar.Coins += 1;
		ui.CoinPickUp();
	}
}
