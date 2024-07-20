using Godot;
using System;

public partial class deckReveal : Control
{
	[ExportGroup("Card Textures")]
	[Export] private Texture2D _spades;
	[Export] private Texture2D _hearts;
	[Export] private Texture2D _diamonds;
	[Export] private Texture2D _clubs;

	private TextureRect cardShown;


	public CardData cardData;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		int rows = Deck.singleton.cards.Count /5;
		VBoxContainer vContainer = new VBoxContainer()
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
            SizeFlagsVertical = SizeFlags.ExpandFill
		};
		GetNode("MarginContainer").AddChild(vContainer);
		
		for(int i = 0; i < rows; i++){
            HBoxContainer container = new HBoxContainer
            {
                SizeFlagsHorizontal = SizeFlags.ExpandFill,
                SizeFlagsVertical = SizeFlags.ExpandFill
            };
            for (int j = 0; j < 5; j++){
                cardShown = new TextureRect
                {
                    ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
                    SizeFlagsHorizontal = SizeFlags.ExpandFill,
                    SizeFlagsVertical = SizeFlags.ExpandFill
                };
				cardData = Deck.singleton.cards[i+j];
				flipCard();
                container.AddChild(cardShown);
			}
			vContainer.AddChild(container);
		}
	}
	public void flipCard(){
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
		cardShown.Texture = singleCardTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
