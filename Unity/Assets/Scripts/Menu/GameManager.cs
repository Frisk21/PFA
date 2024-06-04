using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    

    //GAMEOBJECT
    public GameObject EscapeMenu;

    //VARIABLES
    [HideInInspector] public bool isGameActive;
    public string MyScene;


    void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Affiche le menu de pause
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && isGameActive == true)
        {
            isGameActive = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            EscapeMenu.SetActive(true);
            
        }
        
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EscapeMenu.SetActive(false);
        isGameActive = true;
    }

    public void ReturnToMenu()
    {
        EscapeMenu.SetActive(false);
        SceneManager.LoadScene(MyScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

  
}
