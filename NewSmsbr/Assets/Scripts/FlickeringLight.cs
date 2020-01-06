using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    // Your light gameObject here.
    public Light lomp;
    public int intensity;
    // Array of random values for the intensity.
    private float[] smoothing;

    void Start()
    {
        smoothing = new float[intensity];
        // Initialize the array.
        for (int i = 0; i < smoothing.Length; i++)
        {
            smoothing[i] = .0f;
        }
    }

    void Update()
    {
        float sum = .0f;

        // Shift values in the table so that the new one is at the
        // end and the older one is deleted.
        for (int i = 1; i < smoothing.Length; i++)
        {
            smoothing[i - 1] = smoothing[i];
            sum += smoothing[i - 1];
        }

        // Add the new value at the end of the array.
        smoothing[smoothing.Length - 1] = Random.value;
        sum += smoothing[smoothing.Length - 1];

        // Compute the average of the array and assign it to the
        // light intensity.
        lomp.intensity = sum / smoothing.Length;
    }
}