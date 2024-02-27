using Godot;
using Platformer;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float KnockBackPower = 400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private bool _canDoubleJump = true;
	private bool _hasAdditionalLife = true;
	private Timer _immunityWindowTimer;
	private Timer _jumpEmpowerTimer;
	private Timer _knockbackTimer;
	
	public override void _Ready()
	{
		_jumpEmpowerTimer = GetNode<Timer>("JumpEmpowerTimer");
		_knockbackTimer = GetNode<Timer>("KnockbackTimer");
		_immunityWindowTimer = GetNode<Timer>("ImmunityWindowTimer");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}
		else
		{
			_canDoubleJump = true;
		}
		
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || _canDoubleJump))
		{
			velocity.Y = JumpVelocity;
			if (!IsOnFloor())
			{
				_canDoubleJump = false;
			}
			else
			{
				_jumpEmpowerTimer.Start();
			}
		}

		if (_jumpEmpowerTimer.TimeLeft > 0 && Input.IsActionPressed("jump"))
		{
			velocity.Y -= gravity * 0.75f * (float)delta;
		}

		if (_knockbackTimer.TimeLeft == 0)
		{
			velocity.X = Input.GetAxis("left", "right") * Speed;
		}

		Velocity = velocity;

		MoveAndSlide();
	}

	public void TakeDamage()
	{
		if (_immunityWindowTimer.TimeLeft == 0)
		{
			if (!_hasAdditionalLife)
			{
				GameOver();
			}
			_immunityWindowTimer.Start();
			_hasAdditionalLife = false;
		}
	}

	public void Win()
	{
		GetTree().ReloadCurrentScene();
		GD.Print("You won!");
	}

	public void AddSecondLife()
	{
		_hasAdditionalLife = true;
	}

	public void Knockback(Vector2 enemyPosition)
	{
		var directionFromEnemy = Vector2.Zero;
		var xDistanceFromEnemy = enemyPosition.X - GlobalPosition.X;
		var normalisedDistanceFromEnemy = xDistanceFromEnemy / Mathf.Abs(xDistanceFromEnemy);
		
		directionFromEnemy.X = -normalisedDistanceFromEnemy * KnockBackPower;
		directionFromEnemy.Y = -KnockBackPower / 2;
		
		Velocity = directionFromEnemy;
		_knockbackTimer.Start();
	}

	private void GameOver()
	{
		GlobalVar.Coins = 0;
		GetTree().ReloadCurrentScene();
	}
}
