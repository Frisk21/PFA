using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeManager : MonoBehaviour
{
    //VARIABLES
    [HideInInspector] public bool [] etatCubes;
    [HideInInspector] public bool finish;
    public bool Hub;

    //GAMEOBJECTS
    public GameObject energie;
    public GameObject porteCube; 

    //AUTRES
    [HideInInspector] public Energie enrgieScript;
    [HideInInspector] public porte porteScript;

    // Start is called before the first frame update
    void Start()
    {
        //recup scripts
        if (Hub)
        {
            porteScript = porteCube.GetComponent<porte>();
        }
        else
        {
            enrgieScript = energie.GetComponent<Energie>();
        }

        //init variables
        etatCubes = new bool [7];
        finish = false;

        //init tableau
        for (int j = 0; j<7; j++)
        {
            etatCubes[j] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Hub)
        {
            if (etatCubes[0])
            {
                finish = true;
            }
            if (finish)
            {
                porteScript.OuverturePorte();
            }
        }
        else
        {
            //verifie si tout est true
            finish = true;
            for (int i = 0; i < 7; i++)
            {
                if (!etatCubes[i])
                {
                    finish = false;
                }
            }
            //si tout est bon, active l'energie
            if (finish == true)
            {
                enrgieScript.Active = true;
            }
            
        }
       

        
    }
}
