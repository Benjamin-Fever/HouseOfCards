using Godot;
using System;

public partial class Tutorial : Control
{
	public override void _Input(InputEvent @event){
		if(@event is InputEventKey key){
			if(key.Keycode == Key.Enter && key.IsPressed()){
				Visible = false;
				Main.singleton.deckTimerStart();
			}
		}
	}
}
