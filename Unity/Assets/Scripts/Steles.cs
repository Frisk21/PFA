using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steles : MonoBehaviour
{
    //KEYCODE
    [HideInInspector] public KeyCode E;
    [HideInInspector] public KeyCode AManette;

    //MATERAIL
    [HideInInspector] public Material MatStele;
    [HideInInspector] Color SteleColor;

    //GAMEOBJECT
    public GameObject Player;
    public GameObject stele;
    [HideInInspector] public GameObject enCollision;
    public GameObject Energie;
    public GameObject gameManager;

    //VARIABLES
    [HideInInspector] public int NewEmission;
    [HideInInspector] public float Emission;
    [HideInInspector] public bool hasTouched;
    [HideInInspector] public int speedLum;
    [HideInInspector] public bool isActive;

    //AUTRES
    [HideInInspector] public Energie energieScript;
    [HideInInspector] public GameManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        //Recupere le material et sa couleur d'emission et scripts
        MatStele = stele.GetComponent<Renderer>().material;
        SteleColor = MatStele.GetColor("_EmissionColor");
        energieScript = Energie.GetComponent<Energie>();
        managerScript = gameManager.GetComponent<GameManager>();

        //initialise les emissions et variables
        Emission = 1;
        NewEmission = 5;
        hasTouched = false;
        speedLum = 4;
        E = KeyCode.E;
        AManette = KeyCode.JoystickButton0;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerScript.isGameActive)
        {
            if (hasTouched && (Input.GetKeyDown(E) || Input.GetKeyDown(AManette)))
            {
                isActive = true;
                energieScript.Active = true;
            }
            //change l'emission si on a toucher la stele
            if (Emission < NewEmission && isActive)
            {
                Emission += Time.deltaTime * speedLum;
                MatStele.SetColor("_EmissionColor", SteleColor * Emission);
            }
        }
       
    }

    public void OnTriggerEnter(Collider other)
    {
        //Recupere l'objet toucher
        enCollision = other.gameObject;

        //Quand le player entre en colision avec la stele, active has touched
        if (enCollision == Player)
        {
            hasTouched = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player) 
        {
            hasTouched = false;
        }
    }
}
