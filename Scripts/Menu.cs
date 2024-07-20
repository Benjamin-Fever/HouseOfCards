using Godot;
using System;

public partial class Menu : Control {
	private void OnQuitButtonPressed() {
		GetTree().Quit();
	}
	
	private void OnPlayButtonPressed() {
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
