using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMovement : MonoBehaviour
{
    private Vector3 startPos;
    public float amp;
    public float freq;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);
    }
}
