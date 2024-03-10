using Godot;

public partial class ExtraHealthPickupable : Node2D
{
	private AnimationPlayer _animationPlayer;
	private Timer _idleAnimationTimer;
	private Pickupable _pickupable;
	
	public override void _Ready()
	{
		base._Ready();
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_pickupable = GetNode<Pickupable>("Pickupable");
		_pickupable.DestructionBlocked = true;
	}

	private void OnPickupablePickedUp()
	{
		var player = GetTree().Root.GetNode<CharacterBody2D>("Main/Player");
		_animationPlayer.Play("Pickup");
		player.CallDeferred("AddSecondLife");
	}

	private void OnIdleAnimationTimerTimeout()
	{
		if (!_pickupable.IsPickedUp)
		{
			_animationPlayer.Play("Idle");
		}
	}

	private void OnAnimationPlayerAnimationFinished(StringName animName)
	{
		if (animName == "Pickup")
		{
			_pickupable.DestructionBlocked = false;
		}
	}
}

