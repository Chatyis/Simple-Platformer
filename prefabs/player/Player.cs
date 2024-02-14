using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public Timer jumpEmpowerTimer;

	private bool canDoubleJump;

	public override void _Ready()
	{
		jumpEmpowerTimer = GetNode<Timer>("Timer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}
		else
		{
			canDoubleJump = true;
		}

		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || canDoubleJump))
		{
			if (!IsOnFloor())
			{
				canDoubleJump = false;
				velocity.Y = JumpVelocity;
			}
			else
			{
				velocity.Y = JumpVelocity;
				jumpEmpowerTimer.Start();
			}
		}

		if (jumpEmpowerTimer.TimeLeft > 0f && Input.IsActionPressed("jump"))
		{
			velocity.Y -= gravity * 0.75f * (float)delta;
		}

		velocity.X = Input.GetAxis("left", "right") * Speed;
		Velocity = velocity;

		MoveAndSlide();
	}

	public void GameOver()
	{
		GetTree().ReloadCurrentScene();
	}

	// private void checkIfJumped()
	// {

	// }
}
