using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ToggleSettings()
    {
        if (settings.activeSelf == false)
        {
            settings.SetActive(true);
        }
        else if (settings.activeSelf == true)
        {
            settings.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
