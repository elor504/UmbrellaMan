using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Public Fields
    public float movmentSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    #endregion


    #region Private Fields
    private int currentHp;
    private SpriteRenderer spriteRender;
    bool isCuteScene;
    bool isFacingRight;
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
        if (colliders.Length>0)
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

    }

    public void Flip(float horiznotal)
    {
        if (horiznotal>0 &&!isFacingRight || horiznotal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;
            transform.localScale = scale;
        }
    }


    public void PlayerHp () {

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }



        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }



    public void GetDamage()
    {

    }
}
