using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


      public void OnStartButton() {
        SceneManager.LoadScene("SampleScene"); // Loads your gameplay scene
    }
    public void OnShopButton() {
        // Shop logic will be added later
    }
    public void OnExitButton() {
        Application.Quit();
    }
}
