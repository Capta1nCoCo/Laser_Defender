using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1.5f;

    public void LoadGameOver()
    {
        StartCoroutine(LoadWithDelay());        
    }

    private IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {        
        SceneManager.LoadScene("Level 1");
        if (FindObjectOfType<GameSession>() == null) { return; }
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
