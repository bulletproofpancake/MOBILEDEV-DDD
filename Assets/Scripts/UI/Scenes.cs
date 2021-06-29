using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : Singleton<Scenes>
{
    public void StartGame(string sceneName)
    {
        AudioManager.Instance.Play("button");
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.StartGame();
        
    }

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
