using Godot;
using Platformer;
using System;

public partial class Ui : CanvasLayer
{
	public void CoinPickUp()
	{
		GetNode<Label>("Coins").Text = "Score: " + GlobalVar.Coins.ToString();
	}
}
