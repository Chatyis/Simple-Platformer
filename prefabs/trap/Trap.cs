using Godot;

public partial class Trap : Node2D
{
	private AnimatedSprite2D _animatedSprite2D;
	private DeadlyArea _deadlyArea;
	
	public override void _Ready()
	{
		base._Ready();
		_animatedSprite2D = GetNode<AnimatedSprite2D>("StaticBody2D/AnimatedSprite2D");
		_deadlyArea  = GetNode<DeadlyArea>("StaticBody2D/DeadlyArea");
	}

	private void OnDeadlyAreaBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{	
			_animatedSprite2D.Play();
			_deadlyArea.QueueFree();
		}
	}
}
