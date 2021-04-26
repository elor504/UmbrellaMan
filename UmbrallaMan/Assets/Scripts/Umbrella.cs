﻿using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public int fallingSpeed;

    [Header("Ammonatium related")]
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int currentAmmoAmount;
    [SerializeField]
    List<Projectile> projectilePooling;
    public PlayerManager playerManager;
    bool canUseActiveProjectile;
    [SerializeField]
    float shootingCD;
    [SerializeField]
    float countShootinhCD;
    [SerializeField]
    Projectile projPrefab;
    [SerializeField]
    Transform umbrellaNozzle;
    [SerializeField]
    SpriteRenderer tempSpriteShield;
    [Space(2)]
    [Header("Shield Related")]
    [SerializeField]
    bool isShielding;
    bool isGlide;
    public bool getIsShielding
    {
        set
        {
            if (isShielding != value)
            {
                isShielding = value;
                tempSpriteShield.enabled = isShielding;
            }
        }
        get
        {
            return isShielding;
        }
    }

    bool isAmingUp;
    public bool getIsAimingUp
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
        shootingCountDownCounter();
        OpenUmbrella();
        AimUp();
        Glide();

    }

    void shootingCountDownCounter()
    {
        if (countShootinhCD > 0)
        {
            countShootinhCD -= Time.deltaTime;
        }
        else if (countShootinhCD < 0)
        {
            countShootinhCD = 0;
        }
    }
    public bool CanShoot()
    {
        //checks if player has enough ammo
        if (countShootinhCD == 0 && currentAmmoAmount > 0)
        {
            countShootinhCD = shootingCD;
            return true;
        }
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

    void Glide()
    {
        if (!playerManager.isGrounded && getIsAimingUp)
        {
            // var rb2d = gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
            var rb2d = gameObject.GetComponent<Rigidbody2D>().drag = fallingSpeed;

        }
        else
        {
            var rb2d = gameObject.GetComponent<Rigidbody2D>().drag = 0;
            //  var rb2d = gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        }
    }



    void OpenUmbrella()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!getIsShielding)
                getIsShielding = true;
        }
        else
        {
            if (getIsShielding)
                getIsShielding = false;
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


