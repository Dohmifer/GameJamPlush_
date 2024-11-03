using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab; // Prefab enemy yang akan di-*instantiate*
    public GameObject[] spawnPoints; // Array untuk posisi spawn

    [Header("Player Detection")]
    public LayerMask playerLayer; // Layer untuk mendeteksi player

    private bool hasSpawned = false; // Cek apakah sudah pernah spawn untuk menghindari spawn ulang

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah collider yang menyentuh trigger adalah player
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            // Jika belum pernah spawn sebelumnya
            if (!hasSpawned)
            {
                SpawnEnemies();
                hasSpawned = true; // Set agar tidak spawn ulang
            }
        }
    }

    void SpawnEnemies()
    {
        // Looping untuk memunculkan musuh di setiap titik spawn dalam array
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Visualisasi spawn points di editor
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawWireSphere(spawnPoint.transform.position, 0.5f);
            }
        }
    }
}
