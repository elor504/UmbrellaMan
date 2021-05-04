using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOfMap : MonoBehaviour
{
    public PlayerManager playerManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(("Player")))
        {
            playerManager.Respawn();
        }
    }
}
