using Godot;

public partial class Pickupable : Node2D
{
	[Signal]
	public delegate void PickedUpEventHandler();
	public bool DestructionBlocked;
	public bool IsPickedUp;

	public override void _Ready()
	{
		base._Ready();
		if (!DestructionBlocked && IsPickedUp)
		{
			GD.Print("triggering destruction");
			GetNode("../").QueueFree();
		}
	}

	private void OnArea2DBodyEntered(Node2D body)
	{
		if (!IsPickedUp)
		{
			EmitSignal(SignalName.PickedUp);
			IsPickedUp = true;
		}
	}
}
