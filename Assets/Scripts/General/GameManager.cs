using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameStarter;

    void Awake()
    {
        startScreen.SetActive(true);
        gameStarter.SetActive(false);
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameStarter.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
