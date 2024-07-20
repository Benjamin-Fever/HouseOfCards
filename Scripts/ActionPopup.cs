using Godot;
using System;

public partial class ActionPopup : Control {
	[Signal] public delegate void ActionPassedEventHandler();
	[Signal] public delegate void ActionPlayedEventHandler();

	private void OnActionPassed() {
		EmitSignal(SignalName.ActionPassed);
	}

	private void OnActionPlayed() {
		EmitSignal(SignalName.ActionPlayed);
	}
}
