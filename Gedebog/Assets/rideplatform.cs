using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rideplatform : MonoBehaviour
{
    public GameObject stop;
    public float platformspeed = 5;
    private bool jalan = false;
    private bool balik = false;

    // Update is called once per frame
    void Update()
    {
        if (jalan)
        {
            transform.Translate(platformspeed *Time.deltaTime, 0, 0);
        }
        if (balik)
        {
            muterbalick();
        }
    }

    void OnCollisionEnter2D (Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag ("Player"))
        {
            jalan = true;   
        }
        if (collision2D.gameObject == stop)
        {
            jalan = false;
        } 
    }

    public void kembali()
    {
        balik = true;
    }

    public void muterbalick()
    {
        transform.Translate(-platformspeed *Time.deltaTime, 0, 0);
    }
}
