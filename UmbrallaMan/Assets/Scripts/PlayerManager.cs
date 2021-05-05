using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Public Fields
    public float movmentSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool isGliding;
    public int currentHealth;
    public int maxHealth;
    public float damageTime;
    
  //  public bool CoroutineBreak;
    //public bool gettingHit;
    // public Image[] hearts;  -- Moved to UiManager
    //public Sprite fullHeart; -- Moved to UiManager
    //public Sprite emptyHeart; -- Moved to UiManager

    #endregion


    #region Private Fields

    private bool isRespawning;
    private Vector3 respawnPoint;
    public  GameManager gameManager;
    private SpriteRenderer spriteRender;
    private bool isCuteScene;
    private bool isFacingRight;
    [HideInInspector]
    public Rigidbody2D rb2D;

    

    public float respawnLength;

    #endregion


    Umbrella _playerUmbrella;
   public CameraController _cameraController;
   public PlatformerManager _platofmerManager;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();

        respawnPoint = gameObject.transform.position;
    }

    void Update()
    {
      _cameraController.UpdateCameraPos(gameObject.transform);
        PlayerMovementHandle();
        PlayerHp();

    }


    public void PlayerMovementHandle()
    {
        GroundCheck();
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.position += movement * Time.deltaTime * movmentSpeed;

        //var movement = Input.GetAxis("Horizontal");
        //rb2D.velocity = new Vector2(movement, rb2D.velocity.y) * movmentSpeed * Time.deltaTime;

        Flip(horizontal);
        Jump();

    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    public void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                //rb2D.velocity = Vector2.up * jumpForce;
                rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            }

        }
        #region Testing for better results yet to delete
        //else if ((Input.GetKey(KeyCode.Space)&& !isGrounded))
        //{
        //    isGliding = true;
        //}
        //else
        //{
        //    isGliding = false;
        //}
        #endregion

    }

    public void Flip(float horiznotal)
    {
        if (horiznotal > 0 && !isFacingRight || horiznotal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void PlayerHp()
    {

        #region Moved to other Script for testing yet to deleted.
        //for (int i = 0; i < hearts.Length; i++) -- Moved to UiManager
        //{

        //    if (i < currentHealth)
        //    {
        //        hearts[i].sprite = fullHeart;
        //    }
        //    else
        //    {
        //        hearts[i].sprite = emptyHeart;
        //    }


        //    if (i < maxHealth)
        //    {
        //        hearts[i].enabled = true;
        //    }
        //    else
        //    {
        //        hearts[i].enabled = false;
        //    }
        //}
        #endregion

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            
            //Dead animation 
            //GameOverScreen
            Respawn();
        }
    }

    public void GetDamage(int amount)
    {
      
            currentHealth -= amount;
        
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");

        }


    }

    public void SetCheckPoint(Vector3 newPos)
    {
        respawnPoint = newPos;
        
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;

        yield return new WaitForSecondsRealtime(respawnLength);
        isRespawning = false;

        gameObject.transform.position = respawnPoint;
        GetDamage(1); // does 1 dmg to player?
       // currentHealth = maxHealth; -- reset life back full


    }


    public IEnumerator DealDamagePerTime(int damage)
    {
        //we can add visual that shows that the player got attacked
        if (_platofmerManager.canTakeDmg)
        {
            _platofmerManager.canTakeDmg = false;
            GetDamage(damage);
            yield return new WaitForSecondsRealtime(damageTime);
            _platofmerManager.canTakeDmg = true;

        }
        
    }
}
