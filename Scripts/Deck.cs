using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Godot.Collections;

public partial class Deck : Node2D
{
    [Export] public int deckSize;
    [Export] public int jokerCount;
    [Export] public int pictureCardCount;

	[Export] public PackedScene cardScene;
    // Called when the node enters the scene tree for the first time.
    List<cardData> cards = new List<cardData>();
    public override void _Ready()
    {
        int cardNum;

        for (int i = 0; i < deckSize; i++)
        {
            cardData card;
            if (jokerCount > 0)
            {
                cardNum = 14;
                jokerCount--;
            }
            else if (pictureCardCount > 0)
            {
                int caseNum = (int)GD.RandRange(1, 5);
                switch (caseNum)
                {
                    case 1:
                        cardNum = 10;
                        break;
                    case 2:
                        cardNum = 11; // Jack
                        break;
                    case 3:
                        cardNum = 12; // Queen
                        break;
                    case 4:
                        cardNum = 13; // King
                        break;
                    case 5:
                        cardNum = 1; // Ace
                        break;
                    default:
                        cardNum = 2; 
                        break;
                }
                pictureCardCount--;
            }
            else
            {
                cardNum = (int)GD.RandRange(2, 9); 
            }

            card.value = cardNum;
            card.cardSuit = (cardData.suit)(int)GD.RandRange(0, 4);

            cards.Add(card);
        }

		Shuffle.Shuffler<cardData>(cards);

        foreach (cardData c in cards)
        {
            GD.Print($"Card: {c.value} of {c.cardSuit}");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public override void _Input(InputEvent @event)
    {
        Vector2 mousePos = GetLocalMousePosition();
		if(mousePos.X > -44 && mousePos.X < 44){
			if(mousePos.Y > -70 && mousePos.Y < 70){
				if(@event is InputEventMouseButton mouseButton){
					if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed()){
						card crd = cardScene.Instantiate<card>();
						cardData c = cards[0];
						crd.cardInfo = c;
						AddChild(crd);
						GetViewport().SetInputAsHandled();
					}
				}
			}
		}
    }
}

public struct cardData
{
    public enum suit { Spades, Hearts, Diamonds, Clubs };
    public int value;
    public suit cardSuit;
}

