using Godot;

public partial class PlayerCamera : Camera2D
{
	public double LerpSpeed = 3.0;
	private CharacterBody2D _player;
	
	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("../Player");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Transform = Transform.InterpolateWith(_player.Transform, (float)(LerpSpeed * delta));
	}
}
