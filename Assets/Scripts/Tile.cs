
public struct Tile
{
    public enum TileState {empty, obstructed, road}

    TileState myTileState;
    World myWorld;

    public TileState GetSetTileState{ get { return myTileState;} set { myTileState = value;} }

    int myX;
    int myZ;
    public int GetX { get { return myX; } }
    public int GetZ { get { return myZ; } }
    public Tile(World aWorld, int aX, int aZ)
    {
        myTileState = TileState.empty;
        this.myWorld = aWorld;
        this.myX = aX;
        this.myZ = aZ;

    }


}
