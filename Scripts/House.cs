using Godot;
using System;

public partial class House : Node2D
{
	[Export] private Sprite2D _house;
	[Export] private Texture2D _houseT;
	[Export] public int maxCards = 9;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setTexture(Main.CardCounter);
	}

	private void setTexture(int cardCount){
		int y = 360;
		int x = 390;
		if(cardCount < maxCards){
			y = 360/maxCards * cardCount;
			x = 390/maxCards * cardCount;
		}
		AtlasTexture houseTexture = new AtlasTexture(){
			Atlas = _houseT,
			Region = new Rect2(0,360 - y,x,y)
		};
		_house.Texture = houseTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
