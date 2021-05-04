using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public PoisonCloud cloud;

    public bool IsPlayerIsProtecting(Umbrella umbrella)
    {
        if (umbrella.getIsAimingUp && umbrella.getIsShielding)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log(collision.gameObject.name);
            if (cloud.getIsPurified)
                collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.purified);
            else
                collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.corruptive);
        }
    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            Umbrella umbrella = collision.GetComponent<Umbrella>();
            //if (!IsPlayerIsProtecting(umbrella) && !player.CoroutineBreak)
            //{
            //    //StartCoroutine(player.DealDamagePerTime(1));
            //}
            //else if (IsPlayerIsProtecting(umbrella) && player.CoroutineBreak)
            //{
            //    //StopCoroutine(player.DealDamagePerTime(1));
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
       
        }
    }








}
