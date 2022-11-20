using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    // Fade stuff
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    // Health stuff
    public Image healthBar;

    // Menu stuff
    public GameObject pauseScreen, optionsScreen;
    public Slider musicVolSlier, sfxVolSlider;

    // Tutorial stuff
    public Image[] tutoImages;
    public GameObject tutoScren;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        ManageFade();
    }

    // Controll fade in/out
    // @param none
    // @return void
    private void ManageFade()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    // Display tutorial images
    // @param none
    // @return IEnumerator
    public IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(4.5f);

        tutoImages[0].enabled = true; // this is the background image

        tutoImages[1].enabled = true;
        yield return new WaitForSeconds(8f);

        tutoImages[1].enabled = false;
        tutoImages[2].enabled = true;
        yield return new WaitForSeconds(12f);

        tutoImages[2].enabled = false;
        tutoImages[3].enabled = true;
        yield return new WaitForSeconds(12f);

        tutoImages[3].enabled = false;
        tutoImages[4].enabled = true;
        yield return new WaitForSeconds(6f);

        tutoImages[4].enabled = false;
        tutoImages[0].enabled = false; // disable background image
    }

    /*
     *
     *  BUTTONS FUNCTIONS
     * 
    */
    // Go back to game (from menu)
    // @param none
    // @return void
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    // Open the options menu
    // @param none
    // @return void
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    // Close the options menu
    // @param none
    // @return void
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    // Go back to main menu
    // @param none
    // @return void
    public void Exit()
    {
        Application.Quit();
    }

    // Set music volume from UI
    // @param none
    // @return void
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    // Set sfx volume from UI
    // @param none
    // @return void
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
