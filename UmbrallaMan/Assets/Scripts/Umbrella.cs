using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{



    private PlayerManager playerManager;
    [SerializeField]
    Projectile projPrefab;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        Projectile projectile = Instantiate(projPrefab);
        projectile.InstantiateProjectileSetting(Mathf.RoundToInt(transform.localScale.x), projectileTypes.purified,transform.position);
    }

}
