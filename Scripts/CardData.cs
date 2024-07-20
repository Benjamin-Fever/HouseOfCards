public struct CardData {
    public enum suit { Spades, Hearts, Diamonds, Clubs };
    public int value;
    public suit cardSuit;

    public override string ToString() {
        return value + " of " + cardSuit;
    }
}