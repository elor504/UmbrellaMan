using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField] bool isPurified;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log(collision.gameObject.name);
            if (isPurified)
                collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.purified);
            else
                collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.corruptive);
        }
    }
}
