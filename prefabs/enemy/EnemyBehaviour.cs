using Godot;

public partial class EnemyBehaviour : CharacterBody2D
{
	public const float Speed = 300.0f;
	private bool isMovingLeft = true;
	private AnimatedSprite2D _animatedSprite2D;

	public override void _Ready()
	{
		base._Ready();
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.X = Speed * (isMovingLeft ? -1 : 1);
		
		_animatedSprite2D.FlipH = !isMovingLeft;

		Velocity = velocity;
		MoveAndSlide();
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name != "Player")
		{
			isMovingLeft = !isMovingLeft;
		}
	}
}
