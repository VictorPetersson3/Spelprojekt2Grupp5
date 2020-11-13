
public struct Tile
{
    public enum TileState {empty, obstructed}
    public enum EMyDirections
    {
        North,
        East,
        West,
        South,
        None
    }

    TileState myTileState;
    EMyDirections myDirection;
    World myWorld;

    public TileState GetSetTileState{ get { return myTileState;} set { myTileState = value;} }

    public EMyDirections GetSetDirection { get { return myDirection; }set { myDirection = value; } }

    int myX;
    int myZ;
    public int GetX { get { return myX; } }
    public int GetZ { get { return myZ; } }
    public Tile(World aWorld, int aX, int aZ, EMyDirections aDirection)
    {
        myTileState = TileState.empty;
        this.myWorld = aWorld;
        this.myX = aX;
        this.myZ = aZ;
        myDirection = EMyDirections.None;
    }


}
