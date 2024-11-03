using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFall : MonoBehaviour
{
    public float fallThreshold = -5f; // Batas ketinggian jatuh
    public GameObject Canvasrestart; // Referensi ke tombol restart UI
    [SerializeField] private PlayerMovement playerMovement; // Referensi ke skrip PlayerMovement

    

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Cek posisi Y pemain
        if (transform.position.y < fallThreshold)
        {
            GameOver(); // Panggil metode GameOver jika pemain jatuh
        }
        else if (transform.position.y > fallThreshold)
        {
            Time.timeScale = 1;
        }
    }

    private void GameOver()
    {
        Canvasrestart.SetActive(true); // Tampilkan tombol restart
        Time.timeScale = 0; // Berhentikan waktu
        playerMovement.isGameOver = true; // Set isGameOver ke true
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Mulai waktu kembali
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Memuat ulang scene saat ini
        playerMovement.isGameOver = false; // Set isGameOver ke false agar player dapat bergerak lagi
    }
}
