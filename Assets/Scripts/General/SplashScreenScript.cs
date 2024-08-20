using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    public float timeToShowSplash = 2f;
    public string sceneToLoad = "WorldScene";
    
    void Start()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(timeToShowSplash);

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
