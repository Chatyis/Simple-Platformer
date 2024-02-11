using Godot;

public partial class EnemyBehaviour : CharacterBody2D
{
	public const float Speed = 300.0f;
	public bool isMovingLeft = true;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.X = Speed * (isMovingLeft ? -1 : 1);

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
