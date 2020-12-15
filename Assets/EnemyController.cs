using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<Enemy> myListOfEnemy = new List<Enemy>();

    public void ResetEnemies()
    {
        for (int i = 0; i < myListOfEnemy.Count; i++)
        {
            myListOfEnemy[i].ResetEnemy();
        }
    }


}
