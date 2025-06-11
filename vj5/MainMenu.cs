using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Ensure LoadingScreenManager is properly referenced
        if (LoadingMenuManager.Instance != null)
        {
            LoadingMenuManager.Instance.SwitchToScene(1);
        }
        else
        {
            Debug.LogError("LoadingScreenManager.Instance is null. Ensure it is properly initialized.");
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
