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
				//make player draw
				Card card = Deck.DrawCard();
				card.GlobalPosition += new Vector2(0, 400);
				Deck.singleton.card = card;
				Main.singleton.OnPlayerCardPlayed();
			}
			else if(decidingWeight > 80){
				//ai draws
				Card card = Deck.DrawCard();
			} 
			else{
				if(decidingWeight + (100/deckSize * blankCount) > 60){
					//ai draws
					Card card = Deck.DrawCard();
				}
				else{
					//make player draw
				Card card = Deck.DrawCard();
				card.GlobalPosition += new Vector2(0, 400);
				Deck.singleton.card = card;
				Main.singleton.OnPlayerCardPlayed();
					
				}
			}	
		}
		else{
			//ai draws
			Card card = Deck.DrawCard();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
