using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject hitAudio;
    public GameObject hitEffect;
    public LayerMask hitLayer;

    void Update()
    {
        HitCheck();
        Destroy(gameObject, 5f);
    }

    public void HitCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.2f, transform.position.y), 0.2f, hitLayer);
        foreach (Collider2D ground in colliders)
        {
            GameObject hit = Instantiate(hitAudio, transform.position, Quaternion.identity);
            Destroy(hit, 3f);

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
            Destroy(gameObject);

            // if (ground.GetComponent<PlayerController>() != null)
            // {
            //     ground.GetComponent<PlayerController>().PlayerTakeDamage(10);
            // }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + 0.2f, transform.position.y), 0.2f);
    }
}
