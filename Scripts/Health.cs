using Godot;
using System;

public partial class Health : Node2D {
	[Export] private int MaxHealth = 6;
	[Export] private int CurrentHealth = 6;
	[Export] private Texture2D heartTexture;

	public override void _Ready(){
		UpdateHealthDisplay();
	}

	public void SetHealth(int health){
		CurrentHealth = health;
		UpdateHealthDisplay();
	}

	public void RemoveHealth(int change){
		CurrentHealth -= change;
		if (CurrentHealth > MaxHealth){
			CurrentHealth = MaxHealth;
		}
		if (CurrentHealth < 0){
			CurrentHealth = 0;
		}
		UpdateHealthDisplay();
	}

	public void AddHealth(int change){
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
		// Remove all existing heart nodes
		foreach (Node child in GetChildren()){
			child.Free();
		}
		
		// Add hearts according to MaxHealth
		for (int i = 0; i < MaxHealth/2; i++){
			Sprite2D currentHeart = new Sprite2D();
			SetHeartTexture(currentHeart, 0);
			AddChild(currentHeart);
			currentHeart.Position = new Vector2(i * 210, 0);
		}
		
		// Set hearts to be full, half, or empty
		int healthTaken = MaxHealth - CurrentHealth;
		int heartIndex = -1;
		for (int i = 0; i < (healthTaken % 2 == 0 ? healthTaken/2 : (healthTaken-1)/2); i++){
			heartIndex = i;
			Sprite2D sprite = GetChild<Sprite2D>(i);
			SetHeartTexture(sprite, 2);
		}

		if (healthTaken % 2 != 0){
			Sprite2D sprite = GetChild<Sprite2D>(heartIndex+1);
			SetHeartTexture(sprite, 1);
		}
	}

	private void SetHeartTexture(Sprite2D heartSprite, int heartState){
		AtlasTexture atlasTexture = new AtlasTexture(){ 
			Atlas = heartTexture,
			Region = new Rect2(190 * heartState, 0, 190, 270)
		};

		heartSprite.Texture = atlasTexture;
	}

}
