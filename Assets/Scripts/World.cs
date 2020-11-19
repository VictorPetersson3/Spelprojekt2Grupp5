using UnityEngine;

public class World
{
    int myWidth = 10;
    int myDepth = 10;
    Tile[,] myTiles;
    Road[,] myRoads;
    public World(int aWidth, int aDepth)
    {
        this.myWidth = aWidth;
        this.myDepth = aDepth;


        myTiles = new Tile[myWidth, myDepth];
        myRoads = new Road[myWidth, myDepth];

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

    public void SetRoadState(int aX, int aZ, Road.EMyRoadTypes aRoadType)
    {
        myRoads[aX, aZ].GetSetRoadType = aRoadType;
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

    public Road GetRoadAt(int aX, int aZ)
    {
        if (myRoads[aX, aZ] != null)
        {
            return myRoads[aX, aZ];

        }
        return null;
    }
    public void CopySetTile(Tile aTile)
    {
        myTiles[aTile.GetX, aTile.GetZ] = aTile;
    }

    public void CopySetRoad(Road aRoad)
    {
        myRoads[aRoad.GetSetX, aRoad.GetSetZ] = aRoad;
    }
}

