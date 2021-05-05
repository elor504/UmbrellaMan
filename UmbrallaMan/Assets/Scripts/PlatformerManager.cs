using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MonoBehaviour
{

    public bool canTakeDmg = true;

    public int takeDmgEveryXSeconds;

    public bool heatPlaftorm;
    public bool windPlatform;
    public float gravityUp;
    public float dragUp;
    


    private void OnCollisionStay2D(Collision2D collision)
    {
        Umbrella umbrella = collision.gameObject.GetComponent<Umbrella>();
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

        if (heatPlaftorm)
        {

            if (collision.gameObject.tag == "Player")
            {

                if (playerManager != null && canTakeDmg)
                {
                    //playerManager.GetDamage(1);
                    //StartCoroutine(WaitForSeconds());

                    StartCoroutine(playerManager.DealDamagePerTime(1));

                }
            }

        }
        
        //IEnumerator WaitForSeconds()
        //{
        //    canTakeDmg = false;
        //    yield return new WaitForSecondsRealtime(takeDmgEveryXSeconds);
        //    canTakeDmg = true;
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Umbrella umbrella = collision.gameObject.GetComponent<Umbrella>();
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

        if (windPlatform)
        {
            if (collision.gameObject.tag == "Player")
            {

                if (playerManager.isGrounded && umbrella.getIsAimingUp)
                {
                    playerManager.rb2D.gravityScale = gravityUp;
                    playerManager.rb2D.drag = dragUp;

                }
                else
                {
                    playerManager.rb2D.gravityScale = 1;
                    playerManager.rb2D.drag = 0;
                }



            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        if  (collision.gameObject.tag == "Player")
        {
          playerManager.rb2D.gravityScale = 1;
           playerManager.rb2D.drag = 0;

        }
    }
}
