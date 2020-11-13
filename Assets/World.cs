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
