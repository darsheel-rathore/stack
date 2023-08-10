using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    public bool gameHasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStarted)
            return;

        transform.position += Vector3.up * Time.deltaTime * moveSpeed;
    }
}
