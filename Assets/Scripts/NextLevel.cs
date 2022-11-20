using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel instance;

    private void Awake()
    {
        instance = this;
    }

    // Manage load next scene
    // @param none
    // @return void
    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
