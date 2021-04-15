using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshRoot : MonoBehaviour
{
    public PlayerController playerRoot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            playerRoot.Die();
        }
    }
}
