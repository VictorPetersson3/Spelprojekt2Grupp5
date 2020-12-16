using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    bool myResetEnemyIsActive;

    [SerializeField]
    List<Enemy> myListOfEnemy = new List<Enemy>();

    private void Start()
    {
        myResetEnemyIsActive = false;
    }

    public void ResetEnemies()
    {
        if (myResetEnemyIsActive == false)
        {
            myResetEnemyIsActive = true;

            for (int i = 0; i < myListOfEnemy.Count; i++)
            {
                myListOfEnemy[i].ResetEnemy();
            }
        }
    }


}
