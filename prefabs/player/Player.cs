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
	private bool _canUseCoyoteTime;
	private bool _wasOnFloorBeforeTogglingCoyoteTime;
	private bool _wasInAirBefore;
	private Timer _immunityWindowTimer;
	private Timer _jumpEmpowerTimer;
	private Timer _knockbackTimer;
	private Timer _coyoteTimeTimer;
	private Vector2 _wallJumpDirection = Vector2.Zero;
	private AnimationPlayer _animationPlayer;
	private Sprite2D _sprite2DPlayer;

	public override void _Ready()
	{
		_jumpEmpowerTimer = GetNode<Timer>("JumpEmpowerTimer");
		_knockbackTimer = GetNode<Timer>("KnockbackTimer");
		_immunityWindowTimer = GetNode<Timer>("ImmunityWindowTimer");
		_ui = GetTree().Root.GetNode<CanvasLayer>("Main/Ui");
		_coyoteTimeTimer = GetNode<Timer>("CoyoteTimeTimer");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_sprite2DPlayer =  GetNode<Sprite2D>("Sprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		GravityImpactAndGrounded(ref velocity, delta);
		
		var xMovementDirection = Input.GetAxis("left", "right");
		WallSlide(ref velocity);
		
		Movement(ref velocity, xMovementDirection);
		
		PlayerJump(ref velocity);

		JumpEmpower(ref velocity, delta);

		PlayerSpeedDecay(ref velocity, xMovementDirection, delta);

		CoyoteTime();
		
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
		_ui.CallDeferred("ShowWinMenu");
	}

	public void AddSecondLife()
	{
		_hasAdditionalLife = true;
		_ui.CallDeferred("ToggleExtraLifeIcon", false);
	}

	public void Knockback(Vector2 enemyPosition)
	{
		_animationPlayer.Play("Damage_taken");
		
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
		_ui.CallDeferred("ShowDeathMenu");
	}

	private void JumpEmpower(ref Vector2 velocity, double delta)
	{
		if (_jumpEmpowerTimer.TimeLeft > 0 && Input.IsActionPressed("jump"))
		{
			velocity.Y -= _gravity * 0.75f * (float)delta;
		}
	}
	
	private void WallSlide(ref Vector2 velocity)
	{
		if (IsOnWall() && Velocity.Y > 0)
		{
			for (int i = 0; i < GetSlideCollisionCount(); i++)
			{
				KinematicCollision2D collision = GetSlideCollision(i);
				GD.Print("Collided with: ", collision.GetPosition().X - GlobalPosition.X);
				
				var xDistanceFromWall = collision.GetPosition().X - GlobalPosition.X;
				var normalisedDistanceFromWall = xDistanceFromWall > 1 ? 1 : -1;
				
				_wallJumpDirection.X = normalisedDistanceFromWall * JumpVelocity;
				_wallJumpDirection.Y = JumpVelocity / 1.5f;
			}
			
			velocity.Y *=  0.80f;
			_canWallJump = true;
			_animationPlayer.Play("Wall_slide");
		}
		else if (IsOnFloor())
		{
			_canWallJump = false;
		}
	}

	private void Movement(ref Vector2 velocity, float xMovementDirection)
	{
		if (_knockbackTimer.TimeLeft == 0 && xMovementDirection != 0)
		{
			_wasInAirBefore = false;
			velocity.X = Mathf.MoveToward(velocity.X, xMovementDirection * Speed, 30);
			if (IsOnFloor())
			{
				_animationPlayer.Play("Run");
			}
			_sprite2DPlayer.FlipH = Mathf.Sign(xMovementDirection) == -1;
			_sprite2DPlayer.Offset = new Vector2(Mathf.Sign(xMovementDirection) == -1 ? 1 : 0, 0);
		}
		else if (IsOnFloor() && _knockbackTimer.TimeLeft == 0)
		{
			_animationPlayer.Play(_wasInAirBefore?"Land":"Idle");
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
		var coyoteTimeCheck = _coyoteTimeTimer.TimeLeft > 0 && _canUseCoyoteTime;
		
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || _canDoubleJump || _canWallJump || coyoteTimeCheck))
		{
			if (!IsOnFloor() && !_canWallJump && !coyoteTimeCheck)
			{
				_animationPlayer.Play("Double_jump");
				_canDoubleJump = false;
				velocity.Y = JumpVelocity;
			}
			else if (_canWallJump)
			{
				velocity = _wallJumpDirection;
				_canWallJump = false;
			}
			else
			{
				_animationPlayer.Play("Jump");
				_canUseCoyoteTime = false;
				_jumpEmpowerTimer.Start();
				velocity.Y = JumpVelocity;
			}
		}
	}
	
	private void GravityImpactAndGrounded(ref Vector2 velocity, double delta)
	{
		if (!IsOnFloor() && _coyoteTimeTimer.TimeLeft == 0)
		{
			velocity.Y += _gravity * (float)delta;
			_wasInAirBefore = true;
		}
		else
		{
			_canDoubleJump = true;
		}

		if (velocity.Y > 20)
		{
			_animationPlayer.Play("Fall");
		}
	}

	private void CoyoteTime()
	{
		if (!IsOnFloor() && _wasOnFloorBeforeTogglingCoyoteTime)
		{
			_wasOnFloorBeforeTogglingCoyoteTime = false;
			_coyoteTimeTimer.Start();
		}
		else if (IsOnFloor())
		{
			_wasOnFloorBeforeTogglingCoyoteTime = true;
			_canUseCoyoteTime = true;
		}
	}
	
	private void OnAnimationPlayerAnimationFinished(StringName anim_name)
	{
		if (anim_name == "Land")
		{
			_wasInAirBefore = false;
		}
	}
}

