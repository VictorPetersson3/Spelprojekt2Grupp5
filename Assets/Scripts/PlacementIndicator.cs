using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] myPlacementIndicators;
    //0 = left, 1 = up, 2 = right, 3 = down
    public void CheckPlacementIndicators()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);

        if (x - 1 >= 0)
        {
            if (WorldController.Instance.GetTileAtPosition(x - 1, z).GetSetTileState == Tile.TileState.empty)
            {
                myPlacementIndicators[0].gameObject.SetActive(true);
            }
            else
            {
                myPlacementIndicators[0].gameObject.SetActive(false);
            }
        }
        else
        {
            myPlacementIndicators[0].gameObject.SetActive(false);
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        {
            if (WorldController.Instance.GetTileAtPosition(x + 1, z).GetSetTileState == Tile.TileState.empty)
            {
                myPlacementIndicators[1].gameObject.SetActive(true);
            }
            else
            {
                myPlacementIndicators[1].gameObject.SetActive(false);
            }
        }
        else
        {
            myPlacementIndicators[1].gameObject.SetActive(false);
        }
        if (z - 1 >= 0)
        {
            if (WorldController.Instance.GetTileAtPosition(x, z - 1).GetSetTileState == Tile.TileState.empty)
            {
                myPlacementIndicators[3].gameObject.SetActive(true);
            }
            else
            {
                myPlacementIndicators[3].gameObject.SetActive(false);
            }
        }
        else
        {
            myPlacementIndicators[3].gameObject.SetActive(false);
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (WorldController.Instance.GetTileAtPosition(x, z + 1).GetSetTileState == Tile.TileState.empty)
            {
                myPlacementIndicators[2].gameObject.SetActive(true);
            }
            else
            {
                myPlacementIndicators[2].gameObject.SetActive(false);
            }
        }
        else
        {
            myPlacementIndicators[2].gameObject.SetActive(false);
        }
    }
}
