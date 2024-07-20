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
		// Remove all existing heart nodes
		foreach (Node child in GetChildren()){
			child.QueueFree();
		}
		
		// Add hearts according to MaxHealth
		for (int i = 0; i < MaxHealth; i++){
			Sprite2D currentHeart = heart.Instantiate<Sprite2D>();
			AddChild(currentHeart);
			currentHeart.Position = new Vector2(i * 210, 0);  // Adjust the X position as needed
		}
		
		// Set hearts to be full, half, or empty
		for (int i = 0; i < MaxHealth; i++){
			Sprite2D currentHeart = GetChild<Sprite2D>(i);
			if (CurrentHealth >= i + 2){
				// Full heart
				SetHeartTexture(currentHeart, 0); // Adjust UV coordinates or regions accordingly
			}
			else if (CurrentHealth == i + 1){
				// Half heart
				SetHeartTexture(currentHeart, 1); // Adjust UV coordinates or regions accordingly
			}
			else {
				// Empty heart
				SetHeartTexture(currentHeart, 2); // Adjust UV coordinates or regions accordingly
			}
		}
	}

	private void SetHeartTexture(Sprite2D heartSprite, int heartState){
		AtlasTexture atlasTexture = (AtlasTexture)heartSprite.Texture;

		// Assuming regions are defined in your atlas
		switch (heartState){
			case 0:  // Full heart
				atlasTexture.Region = new Rect2(0, 0, 190, 270);  // Adjust UV coordinates or regions
				break;
			case 1:  // Half heart
				atlasTexture.Region = new Rect2(190, 0, 190, 270);  // Adjust UV coordinates or regions
				break;
			case 2:  // Empty heart
				atlasTexture.Region = new Rect2(380, 0, 190, 270);  // Adjust UV coordinates or regions
				break;
			default:
				break;
		}

		heartSprite.Texture = atlasTexture;
	}

}
