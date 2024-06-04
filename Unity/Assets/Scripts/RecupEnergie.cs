using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupEnergie : MonoBehaviour
{
    //KEYCODES
    [HideInInspector] public KeyCode E;
    [HideInInspector] public KeyCode AManette;

    //GAMEOBJECT
    public GameObject energieSource;
    public GameObject Player;
    public GameObject EndMenu;
    public GameObject FinRobot;
    public GameObject FinVille;
    public GameObject gameManager;

    //VARAIABLES
    [HideInInspector] public bool isTouching;

    //AUTRES
    [HideInInspector] public GameManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        //recup script
        managerScript = gameManager.GetComponent<GameManager>();

        //init variables
        isTouching = false;
        E = KeyCode.E;
        AManette = KeyCode.JoystickButton0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && (Input.GetKeyDown(E) || Input.GetKeyDown(AManette)))
        {
            energieSource.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            EndMenu.SetActive(true);
            managerScript.isGameActive = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            isTouching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            isTouching = false;
        }
    }

    public void ActFinVille()
    {
        EndMenu.SetActive(false);
        FinVille.SetActive(true);
        
    }

    public void ActFinRobot()

    {
        EndMenu.SetActive(false);
        FinRobot.SetActive(true);
    }
}
