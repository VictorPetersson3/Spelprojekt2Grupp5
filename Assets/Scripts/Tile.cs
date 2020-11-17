
public struct Tile
{
    public enum TileState {empty, obstructed, road}
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
    bool myStartTile;
    bool myNeighborIsConnectedToStartTile;

    public TileState GetSetTileState{ get { return myTileState;} set { myTileState = value;} }
    public bool GetSetStartTile{ get { return myStartTile;} set { myStartTile = value;} }
    public bool GetSetNeighborIsConnectedToStartTile { get { return myNeighborIsConnectedToStartTile; } set { myNeighborIsConnectedToStartTile = value;} }

    public EMyDirections GetSetDirection { get { return myDirection; }set { myDirection = value; } }

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
        myDirection = EMyDirections.None;
        myStartTile = false;
        myNeighborIsConnectedToStartTile = false;
    }


}
