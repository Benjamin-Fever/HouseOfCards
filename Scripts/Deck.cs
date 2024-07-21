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

    public Card card;

    public List<CardData> cards = new List<CardData>();
    public static Deck singleton;

    public override void _Ready() {
        singleton = this;
        makeDeck();
    }

    public void makeDeck(){
        cards = new List<CardData>();
        int cardNum;
        int jc = _jokerCount;
        int pc = _pictureCardCount;
        for (int i = 0; i < _deckSize; i++) {
            CardData card;
            if (jc > 0) {
                cardNum = 14;
                jc--;
            }
            else if (pc > 0) {
                int caseNum = GD.RandRange(1, 4);
                switch (caseNum) {
                    case 1:
                        cardNum = 11; // Jack
                        break;
                    case 2:
                        cardNum = 12; // Queen
                        break;
                    case 3:
                        cardNum = 13; // King
                        break;
                    case 4:
                        cardNum = 1; // Ace
                        break;
                    default:
                        cardNum = 2; 
                        break;
                }
                pc--;
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

    public override void _Input(InputEvent @event) {
        if (Main.currentTurn == Main.Turn.OP) { return; }
        Control popup = GetNode<Control>("../CanvasLayer/Popup");
        if(popup.Visible){return;}
        deckReveal reveal = GetNode<deckReveal>("../CanvasLayer/DeckReveal");
        if(reveal.Visible){return;}
        Control reshuffle = GetNode<Control>("../ReshuffleText");
        if(reshuffle.Visible){return;}
        Tutorial tutorial = GetNode<Tutorial>("../Tutorial");
        if(tutorial.Visible){return;}
        Victory victory = GetNode<Victory>("../Victory");
        if(victory.Visible){return;}
        if(GetNode<Control>("../GameOver").Visible){return;}
        Vector2 mousePos = GetLocalMousePosition();

        
        Rect2 rect = sprite.GetRect();
        if(@event is InputEventMouseButton mouseButton && rect.HasPoint(mousePos)){
            if(mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed()){
                Deck.singleton.reshuffle();
                DrawCard();
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
        singleton.card = card;
        return card;
    }

    public static void RevealCard(int revealCount = 1){
        Node2D revealedCards = new Node2D(){
            Name = "RevealedCards"
        };
        singleton.AddChild(revealedCards);
        revealedCards.GlobalPosition = singleton.GlobalPosition;
        for (int i = 0; i < revealCount; i++) {
            Card card = singleton._cardScene.Instantiate<Card>();
            card.cardData = singleton.cards[i];
            card.flipCard();
            revealedCards.AddChild(card);
            card.Position += new Vector2(0, 100 * i);
        }
    }

    public void reshuffle(){
        if(singleton.cards.Count == 0){
            makeDeck();
            Control reshuffle = GetNode<Control>("../ReshuffleText");
            reshuffle.Visible = true;
            Timer reshuffleTimer = GetNode<Timer>("../ReshuffleTimer");
            reshuffleTimer.Start();
        }
    }

    public void reshuffleComplete(){
        Control reshuffle = GetNode<Control>("../ReshuffleText");
        reshuffle.Visible = false;
        deckReveal.singleton.reveal();
        Main.singleton.deckTimerStart();
    }
}



