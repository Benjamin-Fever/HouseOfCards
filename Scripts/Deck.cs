using Godot;
using System.Collections.Generic;

public partial class Deck : Node2D {
    [Export] private int _deckSize = 16;
    [Export] private int _jokerCount = 6;
    [Export] private int _pictureCardCount = 4;

	[Export] private PackedScene _cardScene;

    private List<CardData> cards = new List<CardData>();

    public override void _Ready() {
        int cardNum;
        for (int i = 0; i < _deckSize; i++) {
            CardData card;
            if (_jokerCount > 0) {
                cardNum = 14;
                _jokerCount--;
            }
            else if (_pictureCardCount > 0) {
                int caseNum = (int)GD.RandRange(1, 5);
                switch (caseNum) {
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
                _pictureCardCount--;
            }
            else {
                cardNum = GD.RandRange(2, 9); 
            }

            card.value = cardNum;
            card.cardSuit = (CardData.suit)(int)GD.RandRange(0, 4);

            cards.Add(card);
        }

		Util.Shuffler(cards);
    }

    public override void _Process(double delta) {

    }

    public override void _Input(InputEvent @event) {
        Vector2 mousePos = GetLocalMousePosition();
		if(mousePos.X > -44 && mousePos.X < 44){
			if(mousePos.Y > -70 && mousePos.Y < 70){
				if(@event is InputEventMouseButton mouseButton){
					if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed()){
						Card crd = _cardScene.Instantiate<Card>();
						CardData c = cards[0];
						crd.cardData = c;
						AddChild(crd);
						GetViewport().SetInputAsHandled();
					}
				}
			}
		}
    }
}



