using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject p1;
    public GameObject p2;
    public bool isGameOver = false; 

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
 

    public void GameOver()
    {
        isGameOver = true;

        Invoke("ShowGameOverUI", 5);
    }

    void ShowGameOverUI()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        UiManager.instance.ShowGameOverPanel();
    }
}
