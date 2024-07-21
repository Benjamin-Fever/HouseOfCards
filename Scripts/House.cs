using Godot;
using Godot.Collections;
using System;

public partial class House : Node2D {
	[Export] private Array<Sprite2D> _cards;

	private const int MAXSCORE = 20;

	public override void _Ready() {
		foreach (Sprite2D card in _cards) {
			card.Visible = false;
		}
	}

	public void setTexture(int cardCount){
		for (int i = 0; i < cardCount; i++) {
			if(i >= _cards.Count -1){
				Victory victory = GetNode<Victory>("../Victory");
				victory.houseWin = true;
				victory.displayVictoryBox();
				victory.Visible = true;
			}
			_cards[i].Visible = true;
		}
	}
}
