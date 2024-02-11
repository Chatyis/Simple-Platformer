using Godot;

public partial class DeadlyArea : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			body.CallDeferred("GameOver");
		}
	}
}
