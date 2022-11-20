using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int currentHealth, maxHealth;
    public float invencibleLenght = 2f;
    private float invencibleCounter;

    public Sprite[] healthBarImage; 

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        resetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        blinkDamage();
    }

    // Blinking when receiving damage
    // @param none
    // @return void
    private void blinkDamage()
    {
        if (invencibleCounter > 0)
        {
            invencibleCounter -= Time.deltaTime;
        }
    }
    // Hurt the player
    // And make knock movement
    // @param none
    // @return void

    public void Hurt()
    {
        if (invencibleCounter <= 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.KnockBack();
                invencibleCounter = invencibleLenght;
            }
            updateUI();
        }
    }
    // Reset the health to max
    // @param none
    // @return void
    public void resetHealth()
    {
        currentHealth = maxHealth;
        updateUI();
    }

    // Add Health
    // @param int
    // @return void
    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateUI();
    }

    // Update UI Information
    // @param none
    // @return void
    public void updateUI()
    {
        UpdateHealthImage();
    }

    // Update Health Image
    // @param none
    // @return void
    private void UpdateHealthImage()
    {
        if (currentHealth > 0) { UIManager.instance.healthBar.sprite = healthBarImage[currentHealth - 1]; }
        else { UIManager.instance.healthBar.enabled = false; }
    }

    // Update UI when player dies
    // @param none
    // @return void
    public void PlayerKilled()
    {
        currentHealth = 0;
        updateUI();
    }
}
