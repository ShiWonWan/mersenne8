using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleFade : MonoBehaviour
{
    public static TitleFade instance;


    public float fadeSpeed = 0.01f;
    public Image title;
    public Image blackScreen;
    public Text textoHumano;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f);
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        yield return new WaitForSeconds(3f);
        title.color = new Color(title.color.r, title.color.g, title.color.b, Mathf.MoveTowards(title.color.a, 1f, fadeSpeed * Time.deltaTime));
        yield return new WaitForSeconds(2f);
        if (!TypeWriteEffect.instance.doomed)
        {
            StartCoroutine(TypeWriteEffect.instance.showText());
        }
        yield return new WaitForSeconds(4f);
        textoHumano.color = new Color(textoHumano.color.r, textoHumano.color.g, textoHumano.color.b, Mathf.MoveTowards(textoHumano.color.a, 1f, fadeSpeed * Time.deltaTime));
        yield return new WaitForSeconds(2);
        PlayGame();
    }

    public void PlayGame()
    {
        if (Input.anyKey)
        {
            Debug.Log("Siguiente nivel");
            NextScene();
        }

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
