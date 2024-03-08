using Godot;

public partial class PlatformDecaying : Node2D
{
	private CollisionShape2D _collisionShape2D;
	private Area2D _area2D;
	private Timer _timer;
	private AnimatedSprite2D _animatedSprite2D;
	
	public override void _Ready()
	{
		_collisionShape2D = GetNode<CollisionShape2D>("AnimatableBody2D/CollisionShape2D");
		_area2D = GetNode<Area2D>("AnimatableBody2D/Area2D");
		_timer = GetNode<Timer>("EnableTimer");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatableBody2D/AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		if (_timer.TimeLeft == 0.0f && _collisionShape2D.Disabled)
		{
			EnablePlatform();
		}
	}

	private void OnArea2DBodyExited(Node2D body)
	{
		_animatedSprite2D.Play("decay");
		DisablePlatform();
		_timer.Start();
	}
	
	private void OnArea2DBodyEntered(Node2D body)
	{
		_animatedSprite2D.Play("shake");
	}
	
	private void EnablePlatform()
	{
		_collisionShape2D.SetDeferred("disabled", false);
		_area2D.SetDeferred("monitoring", true);
		_animatedSprite2D.Play("shake");
	}

	private void DisablePlatform()
	{
		_collisionShape2D.SetDeferred("disabled", true);
		_area2D.SetDeferred("monitoring", false);
	}
}
