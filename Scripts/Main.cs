using Godot;
using System;

public partial class Main : Node {
	public enum Turn {
		PLAYER,
		OP
	}
	public static int CardCounter = 0;
	public static Turn currentTurn = Turn.PLAYER;

	public override void _Ready() {
	}

	public override void _Process(double delta) {
	}

	private void OnPlayerCardDrawn() {

	}
}
