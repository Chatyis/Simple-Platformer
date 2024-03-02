using Godot;

public partial class Pickupable : Node2D
{
	[Signal]
	public delegate void PickedUpEventHandler();

	private void OnArea2DBodyEntered(Node2D body)
	{
		EmitSignal(SignalName.PickedUp);
		GetNode("../").QueueFree();
	}
}
