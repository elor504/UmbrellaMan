using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public Color purifiedColor;
    public Color corruptiveColor;

    [SerializeField]
    float projectileSpeed;
    projectileTypes projType;
    int direction;

    [SerializeField]
    float activeTime;
    float currentTime;

    public bool isActive;
    bool isGoingUpward;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentTime = activeTime;

    }

    private void Update()
    {
        if (isActive)
        {
            if(!isGoingUpward)
            rb.velocity = new Vector2((projectileSpeed * Time.deltaTime) * direction, 0);
            else
            rb.velocity = new Vector2(0, (projectileSpeed * Time.deltaTime) * direction);
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                EndProjectile();
            }
        }
    }
    public void InstantiateProjectileSetting(int dir, projectileTypes type, Vector2 playerPos,bool shootingUpward)
    {
        currentTime = activeTime;
        gameObject.SetActive(true);
        isActive = true;
        transform.position = playerPos;
        isGoingUpward = shootingUpward;
        direction = -dir;
        
        ChangeProjectileType(type);
    }

    public void ChangeProjectileType(projectileTypes type)
    {
        projType = type;
        switch (projType)
        {
            case projectileTypes.purified:
                spriteRenderer.color = purifiedColor;
                break;
            case projectileTypes.corruptive:
                spriteRenderer.color = corruptiveColor;
                break;
            default:
                break;
        }



        //change proj visual
    }

    public void EndProjectile()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            if(projType == projectileTypes.corruptive)
            collision.GetComponent<PlayerManager>().GetDamage(1);
    }


}
public enum projectileTypes
{
    purified,
    corruptive
}