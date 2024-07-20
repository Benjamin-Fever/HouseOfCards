using Godot;
using System;

public partial class Main : Node {
	public enum Turn {
		PLAYER,
		OP
	}
	public static int CardCounter = 0;
	public static Turn currentTurn = Turn.PLAYER;
	public static Main singleton;

	public override void _Ready() {
		singleton = this;
	}

	public override void _Process(double delta) {
	}

	private void OnPlayerCardDrawn() {
		Control popup = GetNode<Control>("CanvasLayer/Popup");
		popup.Visible = true;
	}

	public void OnPlayerCardPlayed(){
		Control popup = GetNode<Control>("CanvasLayer/Popup");
		popup.Visible = false;
		Card card = Deck.singleton.card;
		if(card.cardData.value > 1 && card.cardData.value < 11){
			CardCounter++;
			House house = GetNode<House>("House");
			house.setTexture(CardCounter);
		}
		else if(card.cardData.value == 1 || (card.cardData.value > 10 && card.cardData.value < 14)){

		}
		else{
			Health health = GetNode<Health>("Health");
			health.ChangeHealth(-1);
		}
	}
}
