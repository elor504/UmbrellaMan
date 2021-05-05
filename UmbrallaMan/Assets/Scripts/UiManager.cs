using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    #region Public Fields
    public Image[] hearts;
    public PlayerManager playerManager;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    #endregion

    private void Update()
    {
        HeartPreFab();
    }

    void HeartPreFab()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < playerManager.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < playerManager.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }


}
