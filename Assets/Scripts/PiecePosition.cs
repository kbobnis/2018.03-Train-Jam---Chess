
public struct PiecePosition {
    public PieceType type;
    public Position position;
    public int rotation;

    public PiecePosition(PieceType type, Position position, int rotation) {
        this.type = type;
        this.position = position;
        this.rotation = rotation;
    }
}