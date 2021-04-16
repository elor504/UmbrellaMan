using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MonoBehaviour
{

    bool canTakeDmg = true;
    


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var healthComponent = collision.gameObject.GetComponent<PlayerManager>();

            if (healthComponent != null && canTakeDmg)
            {
                healthComponent.GetDamage(1);
                StartCoroutine(WaitForSeconds());
            }
        }



        IEnumerator WaitForSeconds()
        {
            canTakeDmg = false;
            yield return new WaitForSecondsRealtime(3);
            canTakeDmg = true;
        }
    }
}
