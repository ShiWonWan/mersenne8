using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInFadeOut : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack = false, fadeFromBlack = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Fade());
        
    }

    public IEnumerator Fade()
    {
        if (fadeFromBlack)
        {
            yield return new WaitForSeconds(1f);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        fadeFromBlack = false;
        yield return new WaitForSeconds(38f);
        fadeToBlack = true;
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
        yield return new WaitForSeconds(1f);
        NextScene();
    }

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
