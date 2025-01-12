using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Function to load the Game scene
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game"); // Replace "Game" with the exact name of your scene
    }

    // Function to load the Main Menu scene
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace "Main Menu" with the exact name of your scene
    }
}