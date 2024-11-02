using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atasbawah : MonoBehaviour
{
    public float drillSPD;
    public GameObject bt_atas;
    public GameObject bt_bawah;

    bool kebawah = true;

    // Update is called once per frame
    void Update()
    {
        float drilling = drillSPD * Time.deltaTime;
        if (kebawah == true)
        {
            transform.Translate(0, drilling, 0);
        }
        else
        {
            transform.Translate(0, -drilling, 0);
        }

    }

    void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("kena" + col.gameObject.name);
        if (col.gameObject == bt_atas)
        {
            kebawah = true;
            Debug.Log("kebawah");
        }
        if (col.gameObject == bt_bawah)
        {
            kebawah = false;
            Debug.Log("keatas");
        }
    }
}
