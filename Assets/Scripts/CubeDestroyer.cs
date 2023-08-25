using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    // This method is called when a Collider enters the trigger area of the destroyer.
    private void OnTriggerEnter(Collider other)
    {
        // Set a flag in the GameManager to indicate that a collision with the destroyer has occurred.
        GameManager.isCollidedWithDestroyer = true;

        // Reset the reference to the LastCube property in the MovingCube script.
        MovingCube.LastCube = null;

        // Add a Rigidbody component to the collided object to enable physics simulation.
        other.gameObject.AddComponent<Rigidbody>();

        // Disable the BoxCollider component of the collided object to prevent further collisions.
        other.GetComponent<BoxCollider>().enabled = false;

        // Destroy the collided object after a delay of 3 seconds (3f).
        Destroy(other.gameObject, 3f);
    }
}