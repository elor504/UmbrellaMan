using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [Header("Ammonatium related")]
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int currentAmmoAmount;
    
    [Space(5)]

    [Header("")]
    [SerializeField]
    List<Projectile> projectilePooling;
    bool canUseActiveProjectile;

    private PlayerManager playerManager;

    [SerializeField]
    Projectile projPrefab;
    [SerializeField]
    Transform umbrellaNozzle;





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CanShoot())
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddAmmo(5);
        }
    }
    

    public bool CanShoot()
    {
        //checks if player has enough ammo
        if (currentAmmoAmount > 0)
            return true;
        else
            return false;
    }
    void UseAmmo()
    {
        currentAmmoAmount--;
        Mathf.Clamp(currentAmmoAmount, 0, maxAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmoAmount += amount;
        Mathf.Clamp(currentAmmoAmount, 0, maxAmmo);
    }
    public void Shoot()
    {
        UseAmmo();
        canUseActiveProjectile = false;
        if (projectilePooling.Count > 0)
        {
            for (int i = 0; i < projectilePooling.Count; i++)
            {
                if (!projectilePooling[i].isActive)
                {
                    projectilePooling[i].InstantiateProjectileSetting(Mathf.RoundToInt(transform.localScale.x), projectileTypes.purified, umbrellaNozzle.position);
                    canUseActiveProjectile = true;
                    break;
                }
            }
           
        }
        if (!canUseActiveProjectile)
        {
            Projectile projectile = Instantiate(projPrefab);
            projectile.InstantiateProjectileSetting(Mathf.RoundToInt(transform.localScale.x), projectileTypes.purified, umbrellaNozzle.position);
            projectilePooling.Add(projectile);
        }
    }
}


