using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Vector3 respawnPosition;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /// Make invisible the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Set initial respawn position to the spawn (0, 0, 0)
        respawnPosition = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    // Respawn the player
    // @param none
    // @return void
    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnCo()
    {
        // Inactive the player and the camera
        // Then launch black fade transition
        // Launch death effect
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        // Start coroutine, 2 seconds
        yield return new WaitForSeconds(2f);

        // Reset Health
        // Launch the black to normal transition
        // Move the player to the respawn position
        // Enable the camera
        // Finally, enable the player
        HealthManager.instance.resetHealth();
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }

    // Set player spawn point
    // @param Vector3 (spawn position)
    // @return void
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        // Set respawn posotion as given
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Point Set");
    }

    // Manage pause menu
    // @param none
    // @return void
    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            UIManager.instance.tutoScren.SetActive(true);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.tutoScren.SetActive(false);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
