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
	private MarginContainer margin => GetNode<MarginContainer>("MarginContainer");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		int size = (int)Mathf.Sqrt(Deck.singleton.cards.Count);
		VBoxContainer vContainer = new VBoxContainer()
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
            SizeFlagsVertical = SizeFlags.ExpandFill
		};
		GetNode("MarginContainer").AddChild(vContainer);
		
		for(int i = 0; i < size; i++){
            HBoxContainer container = new HBoxContainer
            {
                SizeFlagsHorizontal = SizeFlags.ExpandFill,
                SizeFlagsVertical = SizeFlags.ExpandFill
            };
            for (int j = 0; j < size; j++){
                cardShown = new TextureRect
                {
                    ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
                    SizeFlagsHorizontal = SizeFlags.ExpandFill,
                    SizeFlagsVertical = SizeFlags.ExpandFill
                };
				CardData cardData = Deck.singleton.cards[(i*size)+j];
				GD.Print(Deck.singleton.cards[0].Equals(cardData));
				GD.Print(cardData.value);
				flipCard(cardData);
                container.AddChild(cardShown);
			}
			vContainer.AddChild(container);
		}
	}
	public void flipCard(CardData cardData){
		switch (cardData.cardSuit){
			case CardData.suit.Spades:
				setFrontTexture(_spades, cardData);
				break;
			case CardData.suit.Hearts:
				setFrontTexture(_hearts, cardData);
				break;
			case CardData.suit.Diamonds:
				setFrontTexture(_diamonds, cardData);
				break;
			case CardData.suit.Clubs:
				setFrontTexture(_clubs, cardData);
				break;
		
		}
	}

	private void setFrontTexture(Texture2D cardTexture, CardData cardData){
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
