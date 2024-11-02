using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] item;
    public int randomNumber;
    public int itemNumber;

    void Start()
    {

    }

    public void Drop()
    {
        randomNumber = Random.Range(1, 101);

        if (randomNumber > 1 && randomNumber < 60)
        {
            itemNumber = Random.Range(0, 2);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
        }
        else if (randomNumber > 60 && randomNumber < 85)
        {
            itemNumber = Random.Range(0, 2);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
        }
        else if (randomNumber > 85 && randomNumber < 90)
        {
            itemNumber = Random.Range(0, 2);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
        }
        else if (randomNumber > 90 && randomNumber < 95)
        {
            itemNumber = Random.Range(0, 2);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
        }
        else if (randomNumber > 95 && randomNumber < 100)
        {
            itemNumber = Random.Range(0, 2);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
            Instantiate(item[itemNumber], transform.position, Quaternion.identity);
        }
    }
}
