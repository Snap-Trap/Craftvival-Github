using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Script made by Charly
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); // Loads the scene of the main game
    }

    public void ExitGame()
    {
        Application.Quit(); // Quits the application
    }
}
