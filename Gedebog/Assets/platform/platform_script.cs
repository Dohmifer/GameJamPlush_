using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_script : MonoBehaviour
{
    public float moveSpeed;
    public GameObject start;
    public GameObject finish;
    bool lagimaju = true;
    // Update is called once per frame
    void Update()
    {
        if (lagimaju)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == start)
        {
            lagimaju = true;
        }
        if (coll.gameObject == finish)
        {
            lagimaju = false;
        }
    }


}
