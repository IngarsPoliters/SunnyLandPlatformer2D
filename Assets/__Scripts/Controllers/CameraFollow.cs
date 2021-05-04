using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] private Player player;
   // private Vector2 nextPosition;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //player = FindObjectOfType<Player>();
    }

    void LateUpdate()
    {
        // Store current cameras position in var temp
        Vector3 temp = transform.position;

        //Set the camera position x to equal to players pos x
        temp.x = playerTransform.position.x;

        // Set back cameras temp pos to the cameras current pos
        transform.position = temp;

        //nextPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        //transform.position = new Vector3(nextPosition.x,
        //                                 nextPosition.y,
        //                                 transform.position.z);
    }
}
