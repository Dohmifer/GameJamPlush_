using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotateSPD;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, rotateSPD * Time.deltaTime);
    }
}
