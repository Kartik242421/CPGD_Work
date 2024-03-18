using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] GameObject LevelUI, MainUI;

    public void OpenLevel(int level)
    {
        // Load the scene by index
        SceneManager.LoadScene(level);

        // Disable the LevelUI and enable the MainUI
        LevelUI.SetActive(false);
        MainUI.SetActive(true);
    }
}
