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
		HBoxContainer currentHbox = new HBoxContainer(){ SizeFlagsHorizontal = SizeFlags.ExpandFill, SizeFlagsVertical = SizeFlags.ExpandFill };
		margin.AddChild(currentHbox);
		for (int row = 0; row < size; row++){
			currentHbox.AddChild(new VBoxContainer(){ SizeFlagsHorizontal = SizeFlags.ExpandFill, SizeFlagsVertical = SizeFlags.ExpandFill });
			for (int col = 0; col < size; col++){
				CardData cardData = Deck.singleton.cards[col * size + row];
				TextureRect card = new TextureRect(){
					SizeFlagsHorizontal = SizeFlags.ExpandFill,
					SizeFlagsVertical = SizeFlags.ExpandFill,
					Texture = getTexture(cardData)
				};

				currentHbox.GetChild(0).AddChild(card);
			}
		}
	}


	private AtlasTexture getTexture(CardData cardData){
		Texture2D texture2D = new Texture2D();
		switch (cardData.cardSuit){
			case CardData.suit.Spades:
				texture2D = _spades;
				break;
			case CardData.suit.Hearts:
				texture2D = _hearts;
				break;
			case CardData.suit.Diamonds:
				texture2D = _diamonds;
				break;
			case CardData.suit.Clubs:
				texture2D = _clubs;
				break;
		}

		int y = Mathf.CeilToInt(cardData.value / 5);
		int x = cardData.value % 5;
		AtlasTexture singleCardTexture = new AtlasTexture(){
			Atlas = texture2D,
			Region = new Rect2(190 * x, 270 * y, 190, 270)
		};
		return singleCardTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
