using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public static MainScreen instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public InputField inputField1; 
    public InputField inputField2; 
    public Button saveButton;       
    public Text errorText1;         
    public Text errorText2;         

    public string textFromInputField1;
    public string textFromInputField2;

    void Start()
    {
        
        saveButton.onClick.AddListener(SaveInputs);
    }

    
    void SaveInputs()
    {
        bool inputField1Valid = !string.IsNullOrEmpty(inputField1.text);
        bool inputField2Valid = !string.IsNullOrEmpty(inputField2.text);

        
        if (inputField1Valid)
        {
            textFromInputField1 = inputField1.text;
            errorText1.text = ""; 
        }
        else
        {
            errorText1.text = "Please Input Player 1 Name";
        }

        
        if (inputField2Valid)
        {
            textFromInputField2 = inputField2.text;
            errorText2.text = ""; 
        }
        else
        {
            errorText2.text = "Please Input Player 2 Name";
        }

        if (inputField1Valid && inputField2Valid) {
            SceneManager.LoadScene("Game");
        }
    }
}
