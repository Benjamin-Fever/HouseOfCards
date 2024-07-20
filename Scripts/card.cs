using Godot;
using System;

public partial class Card : Node2D {
	[ExportGroup("Card Textures")]
	[Export] private Texture2D _spades;
	[Export] private Texture2D _hearts;
	[Export] private Texture2D _diamonds;
	[Export] private Texture2D _clubs;
	[ExportGroup("Refrences")]
	[Export] private Sprite2D _cardFront;

	public CardData cardData;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		flipCard();
	}

	private void flipCard(){
		switch (cardData.cardSuit){
			case CardData.suit.Spades:
				setFrontTexture(_spades);
				break;
			case CardData.suit.Hearts:
				setFrontTexture(_hearts);
				break;
			case CardData.suit.Diamonds:
				setFrontTexture(_diamonds);
				break;
			case CardData.suit.Clubs:
				setFrontTexture(_clubs);
				break;
		
		}
	}

	private void setFrontTexture(Texture2D cardTexture){
		int y = Mathf.CeilToInt(cardData.value / 5);
		int x = cardData.value % 5;
		AtlasTexture singleCardTexture = new AtlasTexture(){
			Atlas = cardTexture,
			Region = new Rect2(190 * x, 270 * y, 190, 270)
		};
		_cardFront.Texture = singleCardTexture;
	}
}
