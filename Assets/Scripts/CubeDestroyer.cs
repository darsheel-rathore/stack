using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.isCollidedWithDestroyer = true;
        MovingCube.LastCube = null;

        other.gameObject.AddComponent<Rigidbody>();
        other.GetComponent<BoxCollider>().enabled = false;  
        Destroy(other.gameObject, 3f);
    }
}
