using Godot;
using System;

public partial class Victory : Control
{
	public bool houseWin = false;
	public static Victory singleton;

	public void displayVictoryBox(){
		Label houseText = GetNode<Label>("House");
		Label healthText = GetNode<Label>("Health");
		if(houseWin){
			houseText.Visible = true;
			healthText.Visible = false;
		}
		else{
			houseText.Visible = false;
			healthText.Visible = true;
		}
	}
}
