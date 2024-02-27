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

	public void TogglePausedGameText()
	{
		var pausedLabel = GetNode<Label>("Pause");
		pausedLabel.Visible = !pausedLabel.Visible;
	}
	
	private void UpdateScore()
	{
		GetNode<Label>("Coins").Text = "Score: " + GlobalVar.Coins.ToString();
	}
}
