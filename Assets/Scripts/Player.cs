using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //KEY
    public KeyCode Alt = KeyCode.LeftAlt;

    //VARIABLES
    public float speed;

    public float horizontalInput;
    public float verticalInput;

    public float mouseX;
    public float mouseY;

    //GAMEOBJECT
    public GameObject player;

    //RIGIDBODY
    public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        //Verouille le curseur au centre et le rend invisiblle
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        //Recupere le rigide body du player
        playerRB = player.GetComponent<Rigidbody>();

        //Initialise la vitesse
        speed = 25f;
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



        //recupere la direction
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");



        //Code pour avancer et tourner le player

        //player.transform.position += Quaternion.Euler(0, mouseX, 0) * new Vector3(horizontalInput*speed, 0, verticalInput*speed) ;
        player.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

        playerRB.velocity = Quaternion.Euler(0, mouseX, 0) * new Vector3(horizontalInput * speed, 0, verticalInput * speed);

    }
}
