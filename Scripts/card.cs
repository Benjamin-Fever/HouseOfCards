using Godot;
using System;

public partial class card : Node2D {
	[ExportGroup("Card Textures")]
	[Export] Texture2D spades;
	[Export] Texture2D hearts;
	[Export] Texture2D diamonds;
	[Export] Texture2D clubs;
	[ExportGroup("Refrences")]
	[Export] Sprite2D cardFront;

	public cardData cardInfo;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		switch (cardInfo.cardSuit){
			case cardData.suit.Spades:
				setFrontTexture(spades);
				break;
			case cardData.suit.Hearts:
				setFrontTexture(hearts);
				break;
			case cardData.suit.Diamonds:
				setFrontTexture(diamonds);
				break;
			case cardData.suit.Clubs:
				setFrontTexture(clubs);
				break;
		
		}
	}

	private void setFrontTexture(Texture2D cardTexture){
		int y = Mathf.CeilToInt(cardInfo.value / 5);
		int x = cardInfo.value % 5;
		AtlasTexture singleCardTexture = new AtlasTexture(){
			Atlas = cardTexture,
			Region = new Rect2(88 * x, 124 * y, 88, 124)
		};
		cardFront.Texture = singleCardTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}
}
