using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShader : MonoBehaviour
{
    [SerializeField]
    Material mat;
    Texture2D tex;

    private void Start()
    {
        Invoke("GenerateGridMapTexture", 1.5f);
    }
    private void OnValidate()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }
    public void GenerateGridMapTexture()
    {

        int w, d;
        w = 13;
        d = 13;

        Color[] colors = new Color[w * d];
        if (tex == null)
        {
            tex = new Texture2D(w, d, TextureFormat.RGBA32, false);
        }

        for (int z = 0; z < d; ++z)
        {
            for (int x = 0; x < w; ++x)
            {
                colors[Mathf.FloorToInt( z * w + x)] = Color.black;
             
                if (x > WorldController.Instance.GetWorldWidth || z > WorldController.Instance.GetWorldDepth)
                {
                    colors[Mathf.FloorToInt(z * w + x)] = Color.white;

                }
                if (WorldController.Instance.GetTileAtPosition(x,z).GetSetTileState == Tile.TileState.obstructed)
                {
                    colors[Mathf.FloorToInt(z * w + x)] = Color.white;

                }
            }
        }

        tex.SetPixels(colors);
        tex.Apply();
        mat.SetTexture("Texture2D_9722A4F8", tex);

    }
}
