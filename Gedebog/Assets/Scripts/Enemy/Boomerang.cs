using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public GameObject hitAudio;
    public GameObject hitEffect;
    public LayerMask hitLayer;
    public float speed;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Invoke("DestroyDelay", 2f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        HitCheck();
        Destroy(gameObject, 2f);
    }

    public void HitCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f, hitLayer);
        foreach (Collider2D ground in colliders)
        {
            GameObject hit = Instantiate(hitAudio, transform.position, Quaternion.identity);
            Destroy(hit, 2f);

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);

            //if (ground.GetComponent<PlayerController>() != null)
            {
                //ground.GetComponent<PlayerController>().PlayerTakeDamage(10);
            }
        }
    }

    public void DestroyDelay()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
