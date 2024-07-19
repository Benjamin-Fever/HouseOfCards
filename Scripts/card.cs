using Godot;
using System;

public partial class card : Node2D
{
	public enum suit { Spades, Hearts, Diamonds, Clubs };
	public int value;
	public suit cardSuit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
