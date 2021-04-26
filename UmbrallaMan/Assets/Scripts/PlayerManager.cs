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
    public bool CoroutineBreak;
    public bool gettingHit;
    // public Image[] hearts;  -- Moved to UiManager
    //public Sprite fullHeart; -- Moved to UiManager
    //public Sprite emptyHeart; -- Moved to UiManager

    #endregion


    #region Private Fields
    private SpriteRenderer spriteRender;
    private bool isCuteScene;
    private bool isFacingRight;
    private Rigidbody2D rb2D;

    #endregion


    Umbrella playerUmbrella;
    CameraController cameraController;

    private void Start()
    {


        rb2D = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();

    }

    void Update()
    {

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

    }

    public void GetDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //Dead animation 
            //GameOverScreen
        }
    }

    public IEnumerator DealDamagePerTime(int damage)
    {
        //we can add visual that shows that the player got attacked
        GetDamage(damage);
        yield return new WaitForSeconds(damageTime);
        if (CoroutineBreak)
        {
            StartCoroutine(DealDamagePerTime(1));
        }
    }

}
