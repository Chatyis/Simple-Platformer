using Godot;

public partial class Checkpoint : Area2D
{
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			body.CallDeferred("Win");
		}
	}
}
