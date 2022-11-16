using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osilator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2f;

    private void Awake()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        if(period <=Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSýnWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSýnWave + 1) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }


}
