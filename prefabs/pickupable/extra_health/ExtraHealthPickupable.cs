using Godot;

public partial class ExtraHealthPickupable : Node2D
{
	private void OnPickupablePickedUp()
	{
		var player = GetTree().Root.GetNode<CharacterBody2D>("Main/Player");
		player.CallDeferred("AddSecondLife");
	}
}
