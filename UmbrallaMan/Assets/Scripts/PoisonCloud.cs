using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    #region SerializeField
    [SerializeField] bool isPurified;
    [SerializeField] Color purifiedColor;
    [SerializeField] Color corruptiveColor;
    #endregion

    #region Private Fields
    SpriteRenderer spriteRenderer;
    #endregion 

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            spriteRenderer.color = purifiedColor;
        else
            spriteRenderer.color = corruptiveColor;
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
