
public struct Tile
{
    public enum TileState {empty, obstructed, road}

    TileState myTileState;
    World myWorld;
    Road myRoad;

    public TileState GetSetTileState{ get { return myTileState;} set { myTileState = value;} }
    public Road GetSetRoad{ get { return myRoad; } set { myRoad = value;} }
    
    public void SetRoad (Road aRoad)
    {
        myRoad = aRoad;
    }

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
        myRoad = null;
    }


}
