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
	public static bool doubleDamage = false;

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
		timer.WaitTime = 5;
		timer.Start();
		reveal.Visible = true;
		AIHealth.Visible = false;
		Health.Visible = false;
		deck.Visible = false;
		house.Visible = false;
		GD.Print("Got here");
	}

	public void deckTimerEnd(){
		GD.Print("End time");
		reveal.Visible = false;
		AIHealth.Visible = true;
		Health.Visible = true;
		deck.Visible = true;
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
			if (card.cardData.value == 1){
				// Show 5 cards
				Deck.RevealCard(5);
				Timer timer = new Timer(){
					Autostart = true,
					WaitTime = 2
				};
				currentTurn = Turn.OP;
				AddChild(timer);
				timer.Timeout += () => {
					currentTurn = Turn.PLAYER;
					Deck.singleton.GetNode("RevealedCards").QueueFree();
					timer.QueueFree();
				};
			}
			else if (card.cardData.value == 11){
				// Show 1 card
				Deck.RevealCard();
				currentTurn = Turn.OP;
				Timer timer = new Timer(){
					Autostart = true,
					WaitTime = 2
				};
				AddChild(timer);
				timer.Timeout += () => {
					currentTurn = Turn.PLAYER;
					Deck.singleton.GetNode("RevealedCards").Free();
					timer.QueueFree();
				};
			}
			else if (card.cardData.value == 12){
				Health.AddHealth(1);
			}
			else if (card.cardData.value == 13){
				doubleDamage = true;
			}
		}
		else{
			Health health = GetNode<Health>("Health");
			health.RemoveHealth(1 + (doubleDamage ? 1 : 0));
			doubleDamage = false;
			currentTurn = Turn.OP;
			AI ai = GetNode<AI>("AI");
			ai.OnPlayerDrawsJoker();
		}
		deck.reshuffle();
	}

}
