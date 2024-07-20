using Godot;
using System;

public partial class AI : Node2D
{
	[Export] private int difficulty = 50;
	[Export] private Timer timer;

	private int jokerCount = 0;
	private int powerCardCount = 0;
	private int blankCount = 0;
	private int deckSize = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void OnAiCardDrawn(){
		Main.currentTurn = Main.Turn.OP;
		Control popup = GetNode<Control>("../CanvasLayer/Popup");
		popup.Visible = false;
		Card card = Deck.singleton.card;
		card.GlobalPosition += new Vector2(0, -400);
		card.flipCard();
		checkDeck();
		checkCard();
	}

	public void OnPlayerDrawsJoker(){
		Main.currentTurn = Main.Turn.OP;
		Control popup = GetNode<Control>("../CanvasLayer/Popup");
		popup.Visible = false;
		checkDeck();
		turn();
	}

	private void checkDeck(){
		jokerCount = 0;
		powerCardCount = 0;
		blankCount = 0;
		deckSize = 0;
		foreach(CardData card in Deck.singleton.cards){
			switch(card.value){
				case 1:
					powerCardCount++;
					deckSize++;
					break;
				case 11:
					powerCardCount++;
					deckSize++;
					break;
				case 12:
					powerCardCount++;
					deckSize++;
					break;
				case 13:
					powerCardCount++;
					deckSize++;
					break;
				case 14:
					jokerCount++;
					deckSize++;
					break;
				default:
					blankCount++;
					deckSize++;
					break;
			}
		}
	}

	public void turn(){	
		if(GD.RandRange(0,100) < difficulty){
			int decidingWeight = (100/deckSize * powerCardCount) * 3 - (100/deckSize * blankCount) - (100/deckSize * jokerCount) * 2;

			if(decidingWeight < 50){
				GD.Print("plyaer turn 1");
				//make player draw
				Card card = Deck.DrawCard();
				card.GlobalPosition = new Vector2(0, 200);
				card.Rotation = 0;
				Main.currentTurn = Main.Turn.PLAYER;
				Main.singleton.OnPlayerCardPlayed();
			}
			else if(decidingWeight > 80){
				GD.Print("ai turn");
				//ai draws
				Card card = Deck.DrawCard();
				checkCard();
			} 
			else{
				if(decidingWeight + (100/deckSize * blankCount) > 60){
					GD.Print("ai turn");
					//ai draws
					Card card = Deck.DrawCard();
					checkCard();
				}
				else{
					GD.Print("plyaer turn 2");
					//make player draw
					Card card = Deck.DrawCard();
					card.GlobalPosition += new Vector2(0, 400);
					card.Rotation = 0;
					Main.currentTurn = Main.Turn.PLAYER;
					Main.singleton.OnPlayerCardPlayed();
				}
			}	
		}
		else{
			GD.Print("ai turn");
			//ai draws
			Card card = Deck.DrawCard();
			checkCard();
		}
	}

	public void checkCard(){
		GD.Print("checking card");
		Control popup = GetNode<Control>("../CanvasLayer/Popup");
		popup.Visible = false;
		Card card = Deck.singleton.card;
		card.flipCard();
		if(card.cardData.value > 1 && card.cardData.value < 11){
			//CardCounter++;
			//House house = GetNode<House>("House");
			//house.setTexture(CardCounter);
		}
		else if(card.cardData.value == 1 || (card.cardData.value > 10 && card.cardData.value < 14)){

		}
		else{
			GD.Print("AI got a joker");
			Health health = GetNode<Health>("HealthAI");
			health.RemoveHealth(1);
			// TODO: change turns to player
			Main.currentTurn = Main.Turn.PLAYER;
			return;
		}

		turn();
	}
}
