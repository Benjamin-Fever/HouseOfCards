using Godot;
using System.Collections.Generic;
using System.Net;

public partial class Deck : Node2D {
    [Signal] public delegate void CardDrawnEventHandler();
    [Export] private int _deckSize = 16;
    [Export] private int _jokerCount = 6;
    [Export] private int _pictureCardCount = 4;
    [Export] private Sprite2D sprite;

	[Export] private PackedScene _cardScene;

    public List<CardData> cards = new List<CardData>();
    public static Deck singleton;

    public override void _Ready() {
        singleton = this;
        int cardNum;
        for (int i = 0; i < _deckSize; i++) {
            CardData card;
            if (_jokerCount > 0) {
                cardNum = 14;
                _jokerCount--;
            }
            else if (_pictureCardCount > 0) {
                int caseNum = GD.RandRange(1, 5);
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
            card.cardSuit = (CardData.suit)GD.RandRange(0,3);

            cards.Add(card);
        }

		Util.Shuffler(cards);
    }

    public override void _Process(double delta) {

    }

    public override void _Input(InputEvent @event) {
        if (Main.currentTurn == Main.Turn.OP) { return; }
        Vector2 mousePos = GetLocalMousePosition();
        
        Rect2 rect = sprite.GetRect();
        if(@event is InputEventMouseButton mouseButton && rect.HasPoint(mousePos)){
            if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed()){
                Card card = DrawCard();
                EmitSignal(SignalName.CardDrawn);
			}
		}
    }

    public static Card DrawCard() {
        Card card = singleton._cardScene.Instantiate<Card>();
        card.cardData = singleton.cards[0];
        singleton.cards.RemoveAt(0);
        singleton.AddChild(card);
        if (Main.currentTurn == Main.Turn.OP) {
            card.RotationDegrees = 180;
            card.GlobalPosition += new Vector2(0, -200);
        }
        else {
            card.GlobalPosition += new Vector2(0, 200);
        }
        Timer timer = new Timer(){
            OneShot = true,
            WaitTime = 1,
            Autostart = true
        };
        card.AddChild(timer);
        timer.Timeout += () => {
            GD.Print("Timeout");
            card.flipCard();
        };
        return card;
    }

    public static void RevealCard(int revealCount = 1){
        Node2D revealedCards = new Node2D();
        singleton.AddChild(revealedCards);
        revealedCards.GlobalPosition = singleton.GlobalPosition;
        for (int i = 0; i < revealCount; i++) {
            Card card = singleton._cardScene.Instantiate<Card>();
            card.cardData = singleton.cards[i];
            revealedCards.AddChild(card);
            card.Position += new Vector2(0, 40 * i);
        }
    }
}



