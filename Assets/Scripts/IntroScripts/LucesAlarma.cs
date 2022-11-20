using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesAlarma : MonoBehaviour
{
    float startingPosition;
    [SerializeField] float MovementVector;
    [SerializeField] [Range(0, 30)] float MovementoFactor;
    [SerializeField] float period = 2f;
    public Light Luz;

    private void Awake()
    {
        Luz = GetComponent<Light>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Luz)
            startingPosition = Luz.intensity;

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        MovementoFactor = (rawSinWave + 1f) / 2f; 

        float offset = MovementVector * MovementoFactor;
        if(Luz)
            Luz.intensity = startingPosition + offset;

    }
}
