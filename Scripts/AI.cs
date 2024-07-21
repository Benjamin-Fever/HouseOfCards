using Godot;
using System;

public partial class AI : Node2D
{
	[Export] private int difficulty = 50;

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
		Timer timer = new Timer(){
			WaitTime = 1,
			OneShot = true,
			Autostart = true
		};
		timer.Timeout += () => {
			checkCard();
			checkDeck();
			timer.QueueFree();
		};
		AddChild(timer);
	}

	public void OnPlayerDrawsJoker(){
		Main.currentTurn = Main.Turn.OP;
		Control popup = GetNode<Control>("../CanvasLayer/Popup");
		popup.Visible = false;

		Timer timer = new Timer(){
			WaitTime = 1,
			OneShot = true,
			Autostart = true
		};
		timer.Timeout += () => {
			checkDeck();
			turn();
			timer.QueueFree();
		};
		AddChild(timer);
		
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
		Victory victory = GetNode<Victory>("../Victory");
		if(victory.Visible){return;}
		if(GetNode<Control>("../GameOver").Visible){return;}
		Deck.singleton.reshuffle();

		if(GD.RandRange(0,100) < difficulty){
			int decidingWeight = (100/(deckSize +1) * powerCardCount) * 2 - (100/(deckSize +1)* blankCount) - (100/(deckSize + 1) * jokerCount) * 3;

			if(decidingWeight < 50){
				Deck.singleton.reshuffle();
				GD.Print("Forced Player Turn");
				//make player draw
				Card card = Deck.DrawCard();
				card.GlobalPosition = new Vector2(0, 200);
				card.Rotation = 0;
				Main.currentTurn = Main.Turn.PLAYER;
				Main.singleton.OnPlayerCardPlayed();
			}
			else if(decidingWeight > 80){
				Deck.singleton.reshuffle();
				GD.Print("AI Played Card");
				//ai draws
				Card card = Deck.DrawCard();
				Timer timer = new Timer(){
					WaitTime = 1,
					OneShot = true,
					Autostart = true
				};
				timer.Timeout += () => {
					checkCard();
					timer.QueueFree();
				};
				AddChild(timer);
			} 
			else{
				if(decidingWeight + (100/deckSize * blankCount) > 60){
					Deck.singleton.reshuffle();
					GD.Print("AI Played Card");
					//ai draws
					Card card = Deck.DrawCard();
					Timer timer = new Timer(){
						WaitTime = 1,
						OneShot = true,
						Autostart = true
					};
					timer.Timeout += () => {
						checkCard();
						timer.QueueFree();
					};
			AddChild(timer);
				}
				else{
					Deck.singleton.reshuffle();
					GD.Print("Forced Player Turn");
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
			Deck.singleton.reshuffle();
			GD.Print("ai turn");
			//ai draws
			Card card = Deck.DrawCard();
			Timer timer = new Timer(){
				WaitTime = 1,
				OneShot = true,
				Autostart = true
			};
			timer.Timeout += () => {
				checkCard();
				timer.QueueFree();
			};
			AddChild(timer);
			
		}
	}

	public void checkCard(){
		Control popup = GetNode<Control>("../CanvasLayer/Popup");
		popup.Visible = false;
		Card card = Deck.singleton.card;
		card.flipCard();
		if(card.cardData.value > 1 && card.cardData.value < 11){
		}
		else if(card.cardData.value == 1 || (card.cardData.value > 10 && card.cardData.value < 14)){
			if (card.cardData.value == 12){
				Health health = GetNode<Health>("HealthAI");
				health.AddHealth(1);
			}
			if(card.cardData.value == 13){
				Main.doubleDamage = true;
			}
		}
		else{
			Health health = GetNode<Health>("HealthAI");
			health.RemoveHealth(1 + (Main.doubleDamage ? 1 : 0));
			if(health.CurrentHealth <= 0){
				Victory victory = GetNode<Victory>("../Victory");
				victory.houseWin = false;
				victory.displayVictoryBox();
				victory.Visible = true;
			}
			Main.doubleDamage = false;
			Main.currentTurn = Main.Turn.PLAYER;
			return;
		}
		Timer timer = new Timer(){
			WaitTime = 1,
			OneShot = true,
			Autostart = true
		};
		timer.Timeout += () => {
			turn();
			timer.QueueFree();
		};
		AddChild(timer);
		
	}
}
