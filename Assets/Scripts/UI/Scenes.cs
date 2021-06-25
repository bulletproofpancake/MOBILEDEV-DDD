using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(sceneName);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
