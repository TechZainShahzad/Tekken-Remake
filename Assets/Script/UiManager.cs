using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject gameOverPanel;
    public GameObject gamePausePanel;

    bool isPause = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) && gameOverPanel.active == false)
        {
            isPause = !isPause;
            
            gamePausePanel.SetActive(isPause);

            // If the game is paused, you may want to freeze time or perform other actions
            Time.timeScale = isPause ? 0 : 1;
        }
    }

    public void OnPlayButton()
    {
        isPause = false;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Game");

        // Reset the time scale to its default value
        Time.timeScale = 1;
    }

    public void OnMenuButton()
    {
        
        isPause = false;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");

        // Reset the time scale to its default value
        Time.timeScale = 1;
    }


    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
