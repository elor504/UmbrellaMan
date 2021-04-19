using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    private PlayerManager playerManager;
    [Header("Ammonatium related")]
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int currentAmmoAmount;
    [SerializeField]
    List<Projectile> projectilePooling;
    bool canUseActiveProjectile;



    [SerializeField]
    Projectile projPrefab;
    [SerializeField]
    Transform umbrellaNozzle;
    [Space(2)]
    [Header("Shield Related")]
    [SerializeField]
    bool isShielding;
    public bool getIsShielding => isShielding;

    bool isAmingUp;
    bool getIsAimingUp
    {
        set
        {
            if (isAmingUp != value)
            {
                isAmingUp = value;
                if (isAmingUp)
                    umbrellaNozzle.parent.transform.localRotation = Quaternion.Euler(0, 0, -90);
                else
                    umbrellaNozzle.parent.transform.localRotation = new Quaternion();
            }
        }
        get
        {
            return isAmingUp;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CanShoot() && !isShielding)
        {
            Shoot();
        }

        OpenUmbrella();


        AimUp();
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

    void AimUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!getIsAimingUp)
            {
                getIsAimingUp = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (getIsAimingUp)
            {
                getIsAimingUp = false;
            }
        }
    }

    void OpenUmbrella()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isShielding)
                isShielding = true;
        }
        else
        {
            if (isShielding)
                isShielding = false;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmoAmount += amount;
        if (currentAmmoAmount > maxAmmo)
            currentAmmoAmount = maxAmmo;

        // Mathf.Clamp(currentAmmoAmount, 0, maxAmmo);
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
                    projectilePooling[i].InstantiateProjectileSetting(Mathf.RoundToInt(transform.localScale.x), projectileTypes.purified, umbrellaNozzle.position, getIsAimingUp);
                    canUseActiveProjectile = true;
                    break;
                }
            }

        }
        if (!canUseActiveProjectile)
        {
            Projectile projectile = Instantiate(projPrefab);
            projectile.InstantiateProjectileSetting(Mathf.RoundToInt(transform.localScale.x), projectileTypes.purified, umbrellaNozzle.position, getIsAimingUp);
            projectilePooling.Add(projectile);
        }
    }
}


