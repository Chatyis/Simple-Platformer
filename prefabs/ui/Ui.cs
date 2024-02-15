using Godot;
using Platformer;

public partial class Ui : CanvasLayer
{
	public void CoinPickUp()
	{
		GetNode<Label>("Coins").Text = "Score: " + GlobalVar.Coins.ToString();
	}
}
