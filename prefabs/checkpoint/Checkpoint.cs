using Godot;

public partial class Checkpoint : Area2D
{
	[Export]
	private string NextLevelName;
	[Export]
	private  bool WinningCheckpoint;
	
	private void OnBodyEntered(Node2D body)
	{
		if (!WinningCheckpoint && body.Name == "Player")
		{
			GetTree().ChangeSceneToFile("res://levels/"+NextLevelName+".tscn");
		}
		else if (body.Name == "Player")
		{
			body.CallDeferred("Win");

		}
	}
}
