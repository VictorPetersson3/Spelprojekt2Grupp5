using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailVisulizer : MonoBehaviour
{

   
    enum Piece { straightHorizontal, straightVertical, corss}
    [SerializeField]
    Piece myCurrentPiece;
    void OnDrawGizmos()
    {
        Vector3 currentPos = new Vector3( Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z));
        switch (myCurrentPiece)
        {
            case Piece.corss:

                transform.localScale = new Vector3(0.2f, 0.2f, 1);

                Gizmos.color = Color.black;
                Gizmos.DrawCube(currentPos, transform.localScale);
              
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(currentPos, transform.localScale);
                
                Gizmos.color = Color.black;
                Gizmos.DrawCube(currentPos, new Vector3(transform.localScale.z , transform.localScale.y, transform.localScale.x));
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(currentPos, new Vector3(transform.localScale.z, transform.localScale.y, transform.localScale.x));
                break;

            case Piece.straightHorizontal:
                transform.localScale = new Vector3(0.2f, 0.2f, 1);

                Gizmos.color = Color.black;
                Gizmos.DrawCube(currentPos, transform.localScale);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(currentPos, transform.localScale);

                break;

            case Piece.straightVertical:
                transform.localScale = new Vector3(1, 0.2f, 0.2f);

                Gizmos.color = Color.black;
                Gizmos.DrawCube(currentPos, transform.localScale);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(currentPos, transform.localScale);
                break;
           
        }
    }

}
