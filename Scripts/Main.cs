using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node {

	Timer timer;
	deckReveal reveal;
	Health AIHealth;
	Health Health;
	Deck deck;
	House house;
	Tutorial tutorial;

	public enum Turn {
		PLAYER,
		OP
	}
	public static int CardCounter = 0;
	public static Turn currentTurn = Turn.PLAYER;
	public static Main singleton;

	public override void _Ready() {
		singleton = this;
		getNodes();
		tutorial.Visible = true;
	}

	public void getNodes(){
		timer = GetNode<Timer>("DeckTimer");
		reveal = GetNode<deckReveal>("CanvasLayer/DeckReveal");
		AIHealth = GetNode<Health>("AI/HealthAI");
		Health = GetNode<Health>("Health");
		deck = GetNode<Deck>("Deck");
		house = GetNode<House>("House");
		tutorial = GetNode<Tutorial>("Tutorial");
	}

	public void deckTimerStart(){
		List<CardData> cards = Deck.singleton.cards;
		Util.Shuffler(cards);
		timer.Start();
		reveal.Visible = true;
		AIHealth.Visible = false;
		Health.Visible = false;
		deck.Visible = false;
		house.Visible = false;

	}

	public void deckTimerEnd(){
		deckReveal reveal = GetNode<deckReveal>("CanvasLayer/DeckReveal");
		reveal.Visible = false;
		Health AIHealth = GetNode<Health>("AI/HealthAI");
		AIHealth.Visible = true;
		Health Health = GetNode<Health>("Health");
		Health.Visible = true;
		Deck deck = GetNode<Deck>("Deck");
		deck.Visible = true;
		House house = GetNode<House>("House");
		house.Visible = true;
		List<CardData> cards = Deck.singleton.cards;
		Util.Shuffler(cards);
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
		card.flipCard();
		if(card.cardData.value > 1 && card.cardData.value < 11){
			CardCounter++;
			House house = GetNode<House>("House");
			house.setTexture(CardCounter);
		}
		else if(card.cardData.value == 1 || (card.cardData.value > 10 && card.cardData.value < 14)){

		}
		else{
			Health health = GetNode<Health>("Health");
			health.RemoveHealth(1);
			currentTurn = Turn.OP;
			AI ai = GetNode<AI>("AI");
			ai.OnPlayerDrawsJoker();
		}
	}

}
