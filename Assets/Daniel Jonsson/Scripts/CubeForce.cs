using UnityEngine;

public class CubeForce : MonoBehaviour
{
    Rigidbody myRigidBody;


    public void OnObjectSpawn()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        myRigidBody = gameObject.transform.GetComponent<Rigidbody>();

        myRigidBody.velocity = new Vector3(Random.Range(-5, 5), Random.Range(5, 12), Random.Range(-5, 5));
    }

}
