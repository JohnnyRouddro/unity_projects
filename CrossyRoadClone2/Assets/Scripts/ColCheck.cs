using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColCheck : MonoBehaviour
{
    public GameObject playerRoot;
    private PlayerController playerController;

    public int colCheckType; //0 forward, 1 back, 2 right, 3 left

    private void Start()
    {
        playerController = playerRoot.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerController.obstaclesCheck[colCheckType] = true;
        }

        if (other.CompareTag("MovingPlatform"))
        {
            playerController.movingPlatformsCheck[colCheckType] = true;
            playerController.movingPlatforms[colCheckType] = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerController.obstaclesCheck[colCheckType] = false;
        }

        if (other.CompareTag("MovingPlatform"))
        {
            playerController.movingPlatformsCheck[colCheckType] = false;
        }
    }
}
