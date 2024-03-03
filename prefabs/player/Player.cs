using Godot;
using Platformer;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float KnockBackPower = 400.0f;
	private CanvasLayer _ui;
	private readonly float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private bool _canDoubleJump = true;
	private bool _canWallJump;
	private bool _hasAdditionalLife = true;
	private Timer _immunityWindowTimer;
	private Timer _jumpEmpowerTimer;
	private Timer _knockbackTimer;
	private Vector2 _wallJumpDirection = Vector2.Zero;
	
	public override void _Ready()
	{
		_jumpEmpowerTimer = GetNode<Timer>("JumpEmpowerTimer");
		_knockbackTimer = GetNode<Timer>("KnockbackTimer");
		_immunityWindowTimer = GetNode<Timer>("ImmunityWindowTimer");
		_ui = GetTree().Root.GetNode<CanvasLayer>("Main/Ui");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		GravityImpactAndGrounded(ref velocity, delta);

		PlayerJump(ref velocity);

		JumpEmpower(ref velocity, delta);

		var xMovementDirection = Input.GetAxis("left", "right");
		WallSlide(ref velocity, xMovementDirection, delta);
		
		Movement(ref velocity, xMovementDirection);

		PlayerSpeedDecay(ref velocity, xMovementDirection, delta);

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
			_ui.CallDeferred("ToggleExtraLifeIcon", true);
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
		_ui.CallDeferred("ToggleExtraLifeIcon", false);
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

	private void JumpEmpower(ref Vector2 velocity, double delta)
	{
		if (_jumpEmpowerTimer.TimeLeft > 0 && Input.IsActionPressed("jump"))
		{
			velocity.Y -= _gravity * 0.75f * (float)delta;
		}
	}
	
	private void WallSlide(ref Vector2 velocity, float xMovementDirection, double delta)
	{
		if (Mathf.Sign(_wallJumpDirection.X) != Mathf.Sign(xMovementDirection) && xMovementDirection != 0 && _canWallJump && Velocity.Y > 0)
		{
			velocity.Y *=  0.80f;
		}
	}

	private void Movement(ref Vector2 velocity, float xMovementDirection)
	{
		if (_knockbackTimer.TimeLeft == 0 && xMovementDirection != 0)
		{
			velocity.X = Mathf.MoveToward(velocity.X, xMovementDirection * Speed, 30);
		}
	}

	private void PlayerSpeedDecay(ref Vector2 velocity, float xMovementDirection, double delta)
	{
		if (xMovementDirection == 0)
		{
			velocity.X -= Velocity.X * 10f * (float)delta;
		}
	}
	
	private void PlayerJump(ref Vector2 velocity)
	{
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || _canDoubleJump || _canWallJump))
		{
			if (!IsOnFloor() && !_canWallJump)
			{
				_canDoubleJump = false;
				velocity.Y = JumpVelocity;
			}
			else if (_canWallJump)
			{
				velocity = _wallJumpDirection;
			}
			else
			{
				_jumpEmpowerTimer.Start();
				velocity.Y = JumpVelocity;
			}
		}
	}
	
	private void GravityImpactAndGrounded(ref Vector2 velocity, double delta)
	{
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
		}
		else
		{
			_canDoubleJump = true;
		}
	}

	private void OnWallJumpArea2DBodyEntered(Node2D body)
	{
		if (body.GetGroups().BinarySearch("terrain") > -1)
		{
			var xDistanceFromEnemy = body.GlobalPosition.X - GlobalPosition.X;
			var normalisedDistanceFromEnemy = xDistanceFromEnemy / Mathf.Abs(xDistanceFromEnemy);
		
			_wallJumpDirection.X = normalisedDistanceFromEnemy * JumpVelocity;
			_wallJumpDirection.Y = JumpVelocity / 1.5f;
			
			_canWallJump = true;
		}
	}

	private void OnWallJumpArea2DBodyExited(Node2D body)
	{
		if (body.GetGroups().BinarySearch("terrain") > -1)
		{
			_canWallJump = false;
		}
	}
}

