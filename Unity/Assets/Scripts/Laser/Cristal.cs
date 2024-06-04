using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    //GAMEOBJECT
    public GameObject laser;
    public GameObject cristal;
    public GameObject start;
    public GameObject energie;
    public GameObject portelaser;

    //VARIABLES
    //public int indiceRefractionCristaux;
    //public int indiceRefractionAir;
    //public float Teta1; //angle d'arriver  Donner dans Laser!
    //public float Teta2; //angle de refraction - angle donne dans unity de debut du laser
    [HideInInspector] public bool isTouching; //Donner dans Laser!
    public int angle; //Angle de depart du laser-cristal
    public bool Arrivee; //est ce que c l'arriver? Unity
    public bool Hub;

    //AUTRES
    [HideInInspector] public Laser laserScript; //script du laser qui part du cristaux (pas le meme que celui qui donne les info!)
    [HideInInspector] public Energie EnergieScript;
    [HideInInspector] public porte porteScript;


    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        if (!Arrivee)
        {
            //recup du script
            laserScript = laser.GetComponent<Laser>();
            

            //initialisattion des variables
            //indiceRefractionCristaux = 2;
            //indiceRefractionAir = 1;


            //init de l'angle du laser
            start.transform.rotation = new Quaternion(0, angle, 0, 0);
        }
        else
        {
            if (Hub)
            {
                porteScript = portelaser.GetComponent<porte>();
            }
            else
            {
                EnergieScript = energie.GetComponent<Energie>();
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Arrivee)
        {
            if (isTouching == true)
            {
                //Calcul Teta2
                //Teta2 = Mathf.Asin(indiceRefractionAir * Mathf.Sin(Teta1) / indiceRefractionCristaux);
                laserScript.isActive = true;
            }
            else
            {
                laserScript.isActive = false;
            }
        }
        else
        {
            if (Hub)
            {
                if(isTouching == true)
                {
                    porteScript.OuverturePorte();
                }
            }
            else
            {
                if (isTouching == true)
                {
                    //code de reussite du niveau
                    EnergieScript.Active = true;
                }
            }
            
        }
        

    }

}
