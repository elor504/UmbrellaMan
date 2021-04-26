using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Public Fields
    public Color purifiedColor;
    public bool isActive;
    public Color corruptiveColor;
    #endregion

    #region Private Fields

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    projectileTypes projType;
    float currentTime;
    int direction;
    bool isGoingUpward;

    #endregion

    #region SerializeField
    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float activeTime;

    // only for testing
    [SerializeField]
    bool isTest;
    [SerializeField]
    bool isGoingSideWay;
    #endregion



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentTime = activeTime;
        if (isTest)
        {
            if (!isGoingSideWay)
                InstantiateProjectileSetting(1, projectileTypes.corruptive, this.transform.position, false);
            else
                InstantiateProjectileSetting(1, projectileTypes.corruptive, this.transform.position, true);
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (!isGoingUpward)
                rb.velocity = new Vector2((projectileSpeed * Time.deltaTime) * direction, 0);
            else
                rb.velocity = new Vector2(0, (projectileSpeed * Time.deltaTime) * 1);
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                EndProjectile();
            }
        }
    }
    public void InstantiateProjectileSetting(int dir, projectileTypes type, Vector2 playerPos, bool shootingUpward)
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
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();


            if (projType == projectileTypes.corruptive)
            {
                player.GetDamage(1);
                currentTime = 0;
            }


        }
        else if (collision.gameObject.tag == "Shield")
        {
            
            Umbrella umbrella = collision.transform.parent.parent.GetComponent<Umbrella>();
            if (umbrella.getIsShielding)
            {
                umbrella.AddAmmo(1);
                currentTime = 0;
            }
        }
    }
}

public enum projectileTypes
{
    purified,
    corruptive
}