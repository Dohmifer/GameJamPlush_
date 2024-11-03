using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFall : MonoBehaviour
{
    public float fallThreshold = -5f; // Ketinggian di mana pemain dianggap jatuh
    public GameObject gameOverUI; // Referensi ke objek Game Over
    public GameObject restartButton; // Referensi ke tombol restart
    public PlayerMovement playerMovement; // Referensi ke skrip PlayerMovement

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
    }

    private void GameOver()
    {
        // Menampilkan Game Over UI
        gameOverUI.SetActive(true);
        restartButton.SetActive(true); // Aktifkan tombol restart

        // Menghentikan permainan
        Time.timeScale = 0; // Menonaktifkan semua pergerakan
        playerMovement.isGameOver = true; // Set isGameOver ke true

        Debug.Log("Game Over! Player movement disabled."); // Tambahkan ini
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1; // Menghidupkan kembali waktu
        SceneManager.LoadScene("Lukman"); // Memuat ulang scene saat ini

        playerMovement.isGameOver = false; // Set isGameOver ke false

        Debug.Log("Game Restarted! Player movement enabled."); // Tambahkan ini
    }
}
