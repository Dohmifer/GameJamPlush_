using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public bool inBossBattle = false;
    public GameObject boss;
    public GameObject bossHealth;
    public GameObject bossArena;
    public GameObject bossCamera;
    public GameObject bossCinemachineCamera;
    public GameObject playerCamera;
    public GameObject playerCinemachineCamera;
    public GameObject background;
    public GameObject bossBackground;
    public GameObject enemy;
    public GameObject backgroundMusic;
    public GameObject bossBackgroundMusic;

    void Start()
    {
        boss.SetActive(false);
        bossHealth.SetActive(false);
        bossArena.SetActive(false);
        bossCamera.SetActive(false);
        bossCinemachineCamera.SetActive(false);
        bossBackground.SetActive(false);
        backgroundMusic.SetActive(true);
        bossBackgroundMusic.SetActive(false);
    }

    void Update()
    {
        if (inBossBattle)
        {
            boss.SetActive(true);
            bossHealth.SetActive(true);
            bossArena.SetActive(true);
            bossCamera.SetActive(true);
            bossCinemachineCamera.SetActive(true);
            bossBackground.SetActive(true);

            playerCamera.SetActive(false);
            playerCinemachineCamera.SetActive(false);
            background.SetActive(false);

            enemy.SetActive(false);
            backgroundMusic.SetActive(false);
            bossBackgroundMusic.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inBossBattle = true;
        }
    }
}
