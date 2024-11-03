using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to load a scene by name
    public void Play(string Gameplay_level_test)
    {
        SceneManager.LoadScene(Gameplay_level_test);
    }

    // Method to quit the application
    public void Quit()
    {
        // This will only work in a built application, not in the editor
        Debug.Log("Quit Game"); // Just for confirmation in the editor
        Application.Quit();
    }
}
