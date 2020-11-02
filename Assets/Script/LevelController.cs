using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float delaySecond = 2f;
    
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameManager>().ResetGame();
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator  WaitAndLoad()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
