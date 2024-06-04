using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteract : MonoBehaviour
{
    //KEYCODE
    [HideInInspector] public KeyCode E;
    [HideInInspector] public KeyCode AManette;

    //GAMEOBJECT
    [HideInInspector] public GameObject enCollision;
    [HideInInspector] public GameObject enCollisionExit;
    public GameObject Player;
    public GameObject laser;
    public GameObject gameManager;

    //VARIABLES
    [HideInInspector] public bool isTouching;
    [HideInInspector] public int vitesseRotation;
    [HideInInspector] public float horManette; //joystick manette - QD
    [HideInInspector] public bool isActive;
    public bool BugStart;

    //AUTRES
    [HideInInspector] public Player playerScript;
    [HideInInspector] public GameManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        //recup du script player
        playerScript = Player.GetComponent<Player>();
        managerScript = gameManager.GetComponent<GameManager>();

        //Initialisation des variables
        isTouching = false;
        vitesseRotation = 25;
        E = KeyCode.E;
        AManette = KeyCode.JoystickButton0;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerScript.isGameActive)
        {
            // activer/enlever mode deplacement laser
            if ((Input.GetKeyDown(E) || Input.GetKeyDown(AManette)) && isTouching)
            {
                if (!isActive && !playerScript.playerStop)
                {
                    isActive = true;
                    playerScript.playerStop = true;
                }
                else if (isActive)
                {
                    isActive = false;
                    playerScript.playerStop = false;
                }
            }

            //si ya le deplacement laser
            if (isActive)
            {
                horManette = Input.GetAxis("Horizontal"); // 1 droit - -1 gauche
                                                          //Tourne le laser /!\ pour blocking, mit sur Z aussi!!!! (le start)
                if (isTouching && horManette > 0)
                {
                    if (BugStart)
                    {
                        laser.transform.rotation *= Quaternion.Euler(0, 0, vitesseRotation * Time.deltaTime);
                    }
                    else
                    {
                        laser.transform.rotation *= Quaternion.Euler(0, vitesseRotation * Time.deltaTime, 0);
                    }


                }
                if (isTouching && horManette < 0)
                {
                    if (BugStart)
                    {
                        laser.transform.rotation *= Quaternion.Euler(0, 0, -vitesseRotation * Time.deltaTime);
                    }
                    else
                    {
                        laser.transform.rotation *= Quaternion.Euler(0, -vitesseRotation * Time.deltaTime, 0);
                    }
                }
            }
        }

        
    }

    public void OnTriggerEnter(Collider other)
    {
        //Recupere l'objet toucher
        enCollision = other.gameObject;

        //Quand le player entre en colision avec le laser, avtive isTouching
        if (enCollision == Player)
        {
            isTouching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //recup l'object qui quitte
        enCollisionExit = other.gameObject;

        //si c'est le player, desactive istouching
        if (enCollisionExit == Player)
        {
            isTouching = false;
        }
    }
}
