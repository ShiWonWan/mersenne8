using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHealth;
    public bool playerIsFullHealth;


    /// 
    /// Ocilator
    /// 
    Vector3 startingPosition;
    [SerializeField] Vector3 MovementVector;
    [SerializeField] [Range(0, 1)] float MovementoFactor;
    [SerializeField] float period = 2f;
    public GameObject healthEffect;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 2f, 0f);
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        MovementoFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = MovementVector * MovementoFactor;
        transform.position = startingPosition + offset;

        if (HealthManager.instance.currentHealth == HealthManager.instance.maxHealth)
        {
            playerIsFullHealth = true;
        }
        else
        {
            playerIsFullHealth = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (playerIsFullHealth)
            {
                HealthManager.instance.resetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
                Instantiate(healthEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
                Destroy(gameObject);
            }

            AudioManager.instance.PlaySFX(7);
            

        }
    }
}
