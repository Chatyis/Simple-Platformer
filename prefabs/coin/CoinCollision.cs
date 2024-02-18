using Godot;
using Platformer;

public partial class CoinCollision : Node2D
{
	public CanvasLayer ui;

	public override void _Ready()
	{
		ui = GetTree().Root.GetNode<CanvasLayer>("Main/Ui");
	}

	private void OnArea2DBodyEntered(Node2D body)
	{
		QueueFree();
		GlobalVar.Coins += 1;
		ui.CallDeferred("CoinPickUp");
	}
}
