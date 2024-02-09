using Godot;

public partial class platform_decaying : Node2D
{
	public ColorRect colorRect;
	public CollisionShape2D collisionShape2D;
	public Area2D area2D;
	public Timer timer;

	public override void _Ready()
	{
		colorRect = GetNode<ColorRect>("AnimatableBody2D/ColorRect");
		collisionShape2D = GetNode<CollisionShape2D>("AnimatableBody2D/CollisionShape2D");
		area2D = GetNode<Area2D>("AnimatableBody2D/Area2D");
		timer = GetNode<Timer>("EnableTimer");
	}

	public override void _Process(double delta)
	{
		if (timer.TimeLeft == 0.0f && collisionShape2D.Disabled)
		{
			EnablePlatform();
		}
	}

	private void OnArea2DBodyExited(Node2D body)
	{
		DisablePlatform();
		timer.Start();
	}

	private void EnablePlatform()
	{
		colorRect.Color = new Color(colorRect.Color, 1f);
		collisionShape2D.SetDeferred("disabled", false);
		area2D.SetDeferred("monitoring", true);
	}

	private void DisablePlatform()
	{
		colorRect.Color = new Color(colorRect.Color, 0.25f);
		collisionShape2D.SetDeferred("disabled", true);
		area2D.SetDeferred("monitoring", false);
	}
}
