using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class najib_jalanScript : MonoBehaviour
{
    public float spd;

    // Update is called once per frame
    void Update()
    {
        float jalan = Input.GetAxis("Horizontal") * spd * Time.deltaTime;
        transform.Translate(jalan, 0, 0);  
    }
}
