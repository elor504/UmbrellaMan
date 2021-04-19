﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    [SerializeField] bool isPurified;
    [SerializeField] Color purifiedColor;
    [SerializeField] Color corruptiveColor;
    SpriteRenderer renderer;

    private void Awake()
    {
        if (renderer == null)
            renderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeCloudSprite();
    }

    public void ChangeCloud(bool gettingPurified)
    {
        isPurified = gettingPurified;
        ChangeCloudSprite();
    }

    void ChangeCloudSprite()
    {
        if (isPurified)
            renderer.color = purifiedColor;
        else
            renderer.color = corruptiveColor;
    }

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
