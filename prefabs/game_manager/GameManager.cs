using Godot;

public partial class GameManager : Node2D
{
	public CanvasLayer ui;

	public override void _Ready()
	{
		ui = GetTree().Root.GetNode<CanvasLayer>("Main/Ui");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			GetTree().Paused = !GetTree().Paused;
			ui.CallDeferred("TogglePausedGameText");
		}
	}
}
