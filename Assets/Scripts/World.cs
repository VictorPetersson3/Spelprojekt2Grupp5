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
                myTiles[x, z] = SetTile(x, z, Tile.TileState.empty);
                
                
            }
        }
    }

    public void SetTileDirectionBasedOnStartingTile()
    {
        //Testkod
        for (int x = 0; x < 5; x++)
        {
            myTiles[x, 0].GetSetTileState = Tile.TileState.road;
            
        }
        myTiles[0, 1].GetSetTileState = Tile.TileState.road;
        myTiles[0, 2].GetSetTileState = Tile.TileState.road;
        myTiles[0, 3].GetSetTileState = Tile.TileState.road;
        myTiles[0, 4].GetSetTileState = Tile.TileState.road;
        myTiles[0, 5].GetSetTileState = Tile.TileState.road;



        //slut på testkod
        for (int i = 0; i < myWidth; i++)
        {
            for (int j = 0; j < myDepth; j++)
            {
                if (myTiles[i,j].GetSetTileState == Tile.TileState.road)
                {
                    if (myTiles[i - 1,j].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[i - 1, j].GetSetDirection = Tile.EMyDirections.East;
                    }
                    else if (myTiles[i + 1, j].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[i + 1, j].GetSetDirection = Tile.EMyDirections.West;
                    }
                    else if (myTiles[i, j - 1].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[i, j - 1].GetSetDirection = Tile.EMyDirections.North;
                    }
                    else if (myTiles[i, j + 1].GetSetNeighborIsConnectedToStartTile)
                    {
                        myTiles[i, j + 1].GetSetDirection = Tile.EMyDirections.South;

                    }
                    myTiles[i, j].GetSetNeighborIsConnectedToStartTile = true;
                }
            }
        }
    }

    public Tile SetTile(int aX, int aZ, Tile.TileState aTileState)
    {
        if (aX >= myWidth || aX < 0 || aZ >= myDepth || aZ < 0)
        {
            // Return null "Tile"
            Tile nullTile = new Tile();
            nullTile.GetSetTileState = Tile.TileState.empty;
            return nullTile;
        }

        Tile tempTile = new Tile(this, aX, aZ);
        tempTile.GetSetTileState = aTileState;
        return tempTile;
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
}
