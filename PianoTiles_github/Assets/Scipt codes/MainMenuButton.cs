using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    // Method to load the Main Menu by name
    public void LoadSceneByName()
    {
        Debug.Log("Loading Main Menu scene...");
        SceneManager.LoadScene("Main Menu"); // Make sure the name matches
    }

    // Method to load the first scene in Build Settings
    public void LoadFirstScene()
    {
        Debug.Log("Loading first scene...");
        SceneManager.LoadScene(0); // Loads scene at Build Settings index 0
    }
}
