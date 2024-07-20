using Godot;
using System;

public partial class House : Node2D
{
	[Export] private Sprite2D _house;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setTexture();
	}

	private void setTexture(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
