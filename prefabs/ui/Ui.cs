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
	
	public void ToggleExtraLifeIcon(bool isTransparent)
	{
		var extraLifeIcon = GetNode<TextureRect>("ExtraLife");
		extraLifeIcon.Modulate = new Color(1f, 1f, 1f, isTransparent ? 0.2f : 1f);
	}
	
	private void UpdateScore()
	{
		GetNode<Label>("Coins").Text = "Score: " + GlobalVar.Coins.ToString();
	}
}
