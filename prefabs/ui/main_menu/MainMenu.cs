using Godot;

public partial class MainMenu : Control
{
	private void OnStartBtnPressed()
	{
		GetTree().ChangeSceneToFile("res://main_pause.tscn");
	}

	private void OnExitBtnPressed()
	{
		GetTree().Quit();
	}
}
