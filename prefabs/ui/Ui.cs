using Godot;
using Platformer;

public partial class Ui : CanvasLayer
{
	private Control _gameUi;
	private Control _deathMenuUi;
	
	public override void _Ready()
	{
		base._Ready();
		UpdateScore();
		_gameUi = GetNode<Control>("Game");
		_deathMenuUi = GetNode<Control>("DeathMenu");
	}

	public void CoinPickUp()
	{
		UpdateScore();
	}

	public void TogglePausedGameText()
	{
		var pausedLabel = GetNode<Label>("Game/Pause");
		pausedLabel.Visible = !pausedLabel.Visible;
	}
	
	public void ShowDeathMenu()
	{
		_deathMenuUi.Visible = true;
	}
	
	public void ToggleExtraLifeIcon(bool isTransparent)
	{
		var extraLifeIcon = GetNode<TextureRect>("ExtraLife");
		extraLifeIcon.Modulate = new Color(1f, 1f, 1f, isTransparent ? 0.2f : 1f);
	}

	private void UpdateScore()
	{
		GetNode<Label>("Game/Coins").Text = "Score: " + GlobalVar.Coins;
	}

	private void OnStartBtnPressed()
	{
		GetTree().ReloadCurrentScene();
	}

	private void OnExitBtnPressed()
	{
		GetTree().Quit();
	}
}
