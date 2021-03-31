using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float projectileSpeed;
    projectileTypes projType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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