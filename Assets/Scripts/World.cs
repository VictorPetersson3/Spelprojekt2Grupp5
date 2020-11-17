public class World
{
    int myWidth = 10;
    int myDepth = 10;
    Tile[,] myTiles;

    public World(int aWidth, int aDepth)
    {
        this.myWidth = aWidth;
        this.myDepth = aDepth;

        myTiles = new Tile[myWidth, myDepth];

        for (int x = 0; x < myWidth; x++)
        {
            for (int z = 0; z < myDepth; z++)
            {
                SetTileState(x, z, Tile.TileState.empty);


            }
        }
    }
    public void SetTileState(int aX, int aZ, Tile.TileState aTileState)
    {
        if (aX >= myWidth || aX < 0 || aZ >= myDepth || aZ < 0)
        {
            // Return null "Tile"
            Tile nullTile = new Tile();
            nullTile.GetSetTileState = Tile.TileState.empty;

        }
        else
        {
            Tile tempTile = new Tile(this, aX, aZ);
            tempTile.GetSetTileState = aTileState;
            CopySetTile(tempTile);
        }

    }


    public Tile GetTileAt(int aX, int aZ)
    {
        if (aX >= myWidth || aX < 0 || aZ >= myDepth || aZ < 0)
        {
            // Return null "Tile"
            Tile nullTile = new Tile();
            nullTile.GetSetTileState = Tile.TileState.empty;
            return nullTile;
        }
        return myTiles[aX, aZ];
    }
    public void CopySetTile(Tile aTile)
    {
        myTiles[aTile.GetX, aTile.GetZ] = aTile;
    }

    public void SetTileDirectionBasedOnStartingTile()
    {
        //Testkod
        myTiles[0, 0].GetSetTileState = Tile.TileState.road;
        myTiles[1, 0].GetSetTileState = Tile.TileState.road;
        myTiles[2, 0].GetSetTileState = Tile.TileState.road;
        myTiles[3, 0].GetSetTileState = Tile.TileState.road;
        myTiles[4, 0].GetSetTileState = Tile.TileState.road;
        myTiles[5, 0].GetSetTileState = Tile.TileState.road;

        myTiles[5, 1].GetSetTileState = Tile.TileState.road;
        myTiles[5, 2].GetSetTileState = Tile.TileState.road;
        myTiles[5, 3].GetSetTileState = Tile.TileState.road;
        myTiles[5, 4].GetSetTileState = Tile.TileState.road;
        myTiles[5, 5].GetSetTileState = Tile.TileState.road;



        //slut på testkod
        for (int x = 0; x < myWidth; x++)
        {
            for (int z = 0; z < myDepth; z++)
            {
                if (myTiles[x, z].GetSetTileState == Tile.TileState.road)
                {
                    if (x != 0)
                    {
                        if (myTiles[x - 1, z].GetSetNeighborIsConnectedToStartTile)
                        {
                            myTiles[x - 1, z].GetSetDirection = Tile.EMyDirections.East;
                        }
                    }

                    else if (myTiles[x + 1, z].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[x + 1, z].GetSetDirection = Tile.EMyDirections.West;
                    }
                    if (z != 0)
                    {
                        if (myTiles[x, z - 1].GetSetNeighborIsConnectedToStartTile)
                        {
                            myTiles[x, z - 1].GetSetDirection = Tile.EMyDirections.North;
                        }
                    }
                    else if (myTiles[x, z + 1].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[x, z + 1].GetSetDirection = Tile.EMyDirections.South;

                    }
                    myTiles[x, z].GetSetNeighborIsConnectedToStartTile = true;
                }
            }
        }
    }

    public void SetStartingTileAt(int aX, int aZ, Tile.EMyDirections aDirection)
    {
        myTiles[aX, aZ].GetSetStartTile = true;
        SetTileDirectionAt(aX, aZ, aDirection);
    }

    public void SetTileDirectionAt(int aX, int aZ, Tile.EMyDirections aDirection)
    {
        myTiles[aX, aZ].GetSetDirection = aDirection;
    }
}
