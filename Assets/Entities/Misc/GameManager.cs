using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {


    public void StartGameSurvival()
    {
        SceneManager.LoadScene("Survival");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutor");
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Lose");
    }
}
