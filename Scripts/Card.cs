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
	[Export] private Sprite2D _cardBack;

	public CardData cardData;

	public void flipCard(){
		_cardFront.Visible = true;
		_cardBack.Visible = false;
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
		int y = Mathf.FloorToInt((cardData.value -1) / 5f);
		int x = (cardData.value -1) % 5;
		AtlasTexture singleCardTexture = new AtlasTexture(){
			Atlas = cardTexture,
			Region = new Rect2(190 * x, 270 * y, 190, 270)
		};
		_cardFront.Texture = singleCardTexture;
	}
}
