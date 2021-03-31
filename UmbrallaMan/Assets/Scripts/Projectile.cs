using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float projectileSpeed;
    projectileTypes projType;

    public void ChangeProjectileType(projectileTypes type)
    {
        projType = type;
        //change proj visual
    }


}
public enum projectileTypes
{
    purified,
    corruptive
}