using Godot;
using System;

public partial class Health : Node2D {

	[Export] private int MaxHealth = 6;
	[Export] private int CurrentHealth = 6;
	[Export] private PackedScene heart;

	public override void _Ready(){
		UpdateHealthDisplay();
	}

	public void SetHealth(int health){
		CurrentHealth = health;
		UpdateHealthDisplay();
	}

	public void ChangeHealth(int change){
		CurrentHealth += change;
		if (CurrentHealth > MaxHealth){
			CurrentHealth = MaxHealth;
		}
		if (CurrentHealth < 0){
			CurrentHealth = 0;
		}
		UpdateHealthDisplay();
	}

	private void UpdateHealthDisplay(){
		foreach (Node child in GetChildren()){
			child.QueueFree();
		}
		for (int i = 0; i < MaxHealth; i+=2){
			Sprite2D currentHeart = heart.Instantiate<Sprite2D>();
			AddChild(currentHeart);
			currentHeart.Position = new Vector2(i * 210, 0);
		}
		
		// set hearts to be full, empty or half
		for (int i = 0; i < MaxHealth; i+=2){
			Sprite2D currentHeart = GetChild<Sprite2D>(i/2);
			if (CurrentHealth >= i + 2){
				AtlasTexture atlasTexture = new AtlasTexture(){
					Atlas = ((AtlasTexture)currentHeart.Texture).Atlas,
					Region = new Rect2(0, 0, 190, 270)
				};
				currentHeart.Texture = atlasTexture;
			}
			else if (CurrentHealth == i + 1){
				AtlasTexture atlasTexture = new AtlasTexture(){
					Atlas = ((AtlasTexture)currentHeart.Texture).Atlas,
					Region = new Rect2(190, 0, 190, 270)
				};
				currentHeart.Texture = atlasTexture;
			}
			else {
				AtlasTexture atlasTexture = new AtlasTexture(){
					Atlas = ((AtlasTexture)currentHeart.Texture).Atlas,
					Region = new Rect2(380, 0, 190, 270)
				};
				currentHeart.Texture = atlasTexture;
			}
		}
	}
}
