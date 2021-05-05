using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{

    [SerializeField] bool isPurified;
    [SerializeField] Color purifiedColor;
    [SerializeField] Color corruptiveColor;
    [SerializeField] Collider2D[] colliders;
    [SerializeField] ParticleSystem particles;



    SpriteRenderer spriteRenderer;


    //Timer
    [SerializeField]
    float purificationTime;
    float purificationCD;
    bool gotPurified;

    public bool getIsPurified => isPurified;
    private void Awake()
    {
       
        if (spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChangeCloudSprite();
    }


    private void Update()
    {
        if (gotPurified)
        {
            if (purificationCD > 0)
            {
                purificationCD -= Time.deltaTime;
            }
            else if (purificationCD < 0)
            {
                gotPurified = false;
                purificationCD = 0;
                ChangeCloud(false);
            }
        }
    }

    public void ChangeCloud(bool gettingPurified)
    {
        isPurified = gettingPurified;
        ChangeCloudSprite();
    }

    void ChangeCloudSprite()
    {
        if (isPurified)
        {
            spriteRenderer.color = purifiedColor;
            particles.startColor = new Color(255, 255,255, 255);
        }
        else
        {
            spriteRenderer.color = corruptiveColor;
            particles.startColor = new Color(0, 255, 0, 255);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{
			purificationCD = purificationTime;
			Projectile projectile = collision.gameObject.GetComponent<Projectile>();
			projectile.EndProjectile();
			gotPurified = true;
			ChangeCloud(true);
		}
	}

}
