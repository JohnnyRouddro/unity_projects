using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    public float movementWavelength;
    public float movementAmplitude;
    public float movementPhase;
    public Vector3 currentPos;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementPhase += Time.fixedDeltaTime;

        //using this function for simple harmonic motion https://www.desmos.com/calculator/yi8wgigewg

        currentPos.x = 2 * movementAmplitude / Mathf.PI * Mathf.Asin(Mathf.Sin(2 * Mathf.PI / movementWavelength * movementPhase + Mathf.PI / 2));
        transform.position = currentPos + offset;
    }
}
