using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject hitAudio;
    public GameObject hitEffect;
    public LayerMask hitLayer;

    void Start()
    {
        Destroy(gameObject, 5f); // Hancurkan projectile setelah 5 detik jika tidak mengenai apapun
    }

    void Update()
    {
        HitCheck();
    }

    public void HitCheck()
    {
        // Memeriksa apakah projectile mengenai objek pada layer yang ditentukan (hitLayer)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.2f, transform.position.y), 0.2f, hitLayer);
        foreach (Collider2D target in colliders)
        {
            // Mainkan audio dan efek saat mengenai target
            if (hitAudio != null)
            {
                GameObject hit = Instantiate(hitAudio, transform.position, Quaternion.identity);
                Destroy(hit, 3f);
            }

            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }

            Destroy(gameObject); // Hancurkan projectile setelah mengenai target
            break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // Menampilkan radius deteksi hit di Editor
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + 0.2f, transform.position.y), 0.2f);
    }
}
