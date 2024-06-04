using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //KEY
    //Clavier + souris
    [HideInInspector] public KeyCode Alt = KeyCode.LeftAlt;

    //VARIABLES
    [HideInInspector] public float speed;

    //Deplacement: Joystick gauche / ZQSD
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    // Camera
    [HideInInspector] public float AxeXCam;
    [HideInInspector] public float AxeYCam;
    [HideInInspector] public int AxeYMax;
    [HideInInspector] public int AxeYMin;
    // Clavier + souris
    [HideInInspector] public float mouseX;
    [HideInInspector] public float mouseY;
    // Manette
    [HideInInspector] public float horizontalInputManette;
    [HideInInspector] public float verticalInputManette;

    //Saut
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float taille;
    [HideInInspector] public int sautSpeed;
    [HideInInspector] public bool playerStop;


    //GAMEOBJECT
    public GameObject player;
    public GameObject LookPoint;
    public GameObject playerFeet;
    public GameObject GameManager;

    //INFO GO
    [HideInInspector] public Rigidbody playerRB;
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public GameManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation des keys
        Alt = KeyCode.LeftAlt;

        //Verouille le curseur au centre et le rend invisiblle
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        //Recupere le rigide body du player
        playerRB = player.GetComponent<Rigidbody>();
        
        //recup script
        managerScript = GameManager.GetComponent<GameManager>();



        //Initialise les variables
        playerStop = false;
        speed = 6f;
        taille = 0.1f;
        sautSpeed = 30000; // (fall speed)
        AxeYMax = 25;
        AxeYMin = -50;
    }

    // Update is called once per frame
    void Update()
    {
        //Souris libre
        if (Input.GetKey(Alt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (managerScript.isGameActive)
        {

            //recupere la direction
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            horizontalInputManette = Input.GetAxis("Horizontal Manette");
            verticalInputManette = Input.GetAxis("Vertical Manette");
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            AxeXCam += mouseX + horizontalInputManette;
            AxeYCam += mouseY + verticalInputManette;

            //regade si YCam n'est pas au max
            if (AxeYCam > AxeYMax)
            {   
                AxeYCam = AxeYMax;
            }
            if (AxeYCam < AxeYMin)
            {
                AxeYCam = AxeYMin;
            }

        

            //Code pour avancer et tourner le player
            player.transform.rotation = Quaternion.Euler(0, AxeXCam, 0);
            LookPoint.transform.rotation = Quaternion.Euler(-AxeYCam,AxeXCam, 0);

            if (!playerStop)
            {
                playerRB.velocity = Quaternion.Euler(0, AxeXCam, 0) * new Vector3(horizontalInput * speed, 0, verticalInput * speed);
            }
            else
            {
                playerRB.velocity = Vector3.zero;
            }
      



            //check si touche le sol
            if (Physics.Raycast(playerFeet.transform.position, Vector3.down, out hit, taille))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (!isGrounded)
            {
                playerRB.AddRelativeForce(Vector3.down * (sautSpeed * Time.deltaTime));
            }
        }
        else
        {
            playerRB.velocity = Vector3.zero;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        playerRB.velocity += Vector3.zero;
      
    }
    
    public void PlayerTP(GameObject posFinal)
    {
        playerRB.position = posFinal.transform.position;
    }
}
