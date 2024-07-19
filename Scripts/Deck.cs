using Godot;
using System;
using System.Collections;

public partial class Deck : Node2D
{
	[Export] public int deckSize;
	[Export] public int jokerCount;
	[Export] public int pictureCardCount;
	// Called when the node enters the scene tree for the first time.
	ArrayList cards = new ArrayList();
	public override void _Ready()
	{
		for(int i = 0; i < deckSize; i++){
			if(jokerCount > 0){
					
			}
			else if(pictureCardCount > 0){

			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}

public struct cardData {
	public enum suit { Spades, Hearts, Diamonds, Clubs };
	public int value;
	public suit cardSuit;
}