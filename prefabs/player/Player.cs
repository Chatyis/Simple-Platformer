using Godot;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{

			velocity.Y += gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		velocity.X = Input.GetAxis("left", "right") * Speed;
		Velocity = velocity;

		MoveAndSlide();
	}

	public void GameOver()
	{
		GetTree().ReloadCurrentScene();
	}

	public void Win()
	{
		GetTree().ReloadCurrentScene();
		GD.Print("You won!");
	}
}
