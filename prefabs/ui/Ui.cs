using Godot;
using Platformer;

public partial class Ui : CanvasLayer
{
	public override void _Ready()
	{
		base._Ready();
		UpdateScore();
	}

	public void CoinPickUp()
	{
		UpdateScore();
	}

	private void UpdateScore()
	{
		GetNode<Label>("Coins").Text = "Score: " + GlobalVar.Coins.ToString();
	}
}
