using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public PlayerManager playerManager;
    void Awake()
    {
        if (gameObject == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameManager);
        }
        else
        {
            Destroy(gameManager);
        }

    }

   public void GameOver()
    {
        Debug.Log("Your Dead - Click H to Respawn");
        if (Input.GetKeyDown((KeyCode.H)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
